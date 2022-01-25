using UnityEngine;
using Firebase;
using System.Collections;
using Firebase.Auth;
using TMPro;
using System.IO;
using Firebase.Database;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance;

    [Header("Firebase")]
    public FirebaseAuth auth;
    public FirebaseUser user;
    public DatabaseReference DbRefrence;
    [Space(5f)]

    [Header("Login Refrences")]
    [SerializeField] private TMP_InputField loginEmail;
    [SerializeField] private TMP_InputField loginPassword;
    [SerializeField] private TMP_Text loginOutputText;
    [Space(5f)]

    [Header("Register Refrences")]
    [SerializeField] private TMP_InputField registerUsername;
    [SerializeField] private TMP_InputField registerEmail;
    [SerializeField] private TMP_InputField registerPassword;
    [SerializeField] private TMP_InputField registerConfirmPassword;
    [SerializeField] private TMP_Text registerOutputText;

    [Header("Scenes")]
    [SerializeField] private int loadSceneIndex = 1;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(checkDependancyTask =>
        {
            var dependancyStatus = checkDependancyTask.Result;

            if(dependancyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependancyStatus}");
            }
        });
    }

    private void Start()
    {
        StartCoroutine(CheckAndFixDependencies());
    }


    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        StartCoroutine(CheckAutoLogin());

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
        DbRefrence = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private IEnumerator CheckAndFixDependencies()
    {
        yield return new WaitForSeconds(2f);

        var checkAndFixDependanciesTask = FirebaseApp.CheckAndFixDependenciesAsync();

        yield return new WaitUntil(predicate: () => checkAndFixDependanciesTask.IsCompleted);

        var dependancyResult = checkAndFixDependanciesTask.Result;

        if(dependancyResult == DependencyStatus.Available)
        {
            InitializeFirebase();
        }
        else
        {
            Debug.LogError($"Could not resolve all Firebase dependancies: {dependancyResult}");
        }
    }

    private IEnumerator CheckAutoLogin()
    {
        yield return new WaitForEndOfFrame();

        if(user != null)
        {
            var reloadUserTask = user.ReloadAsync();

            yield return new WaitUntil(predicate: () => reloadUserTask.IsCompleted);

            AutoLogin();
        }
        else
        {
            AuthUiManager.instance.LoginScreen();
        }
    }

    private void AutoLogin()
    {
        if(user != null)
        {
            if (user.IsEmailVerified)
            {
                if (!Directory.Exists($"{Application.persistentDataPath}/Tips/"))
                {
                    Directory.CreateDirectory($"{Application.persistentDataPath}/Tips/");
                    if (!File.Exists($"{Application.persistentDataPath}/Tips/Tips.txt"))
                    {
                        File.CreateText($"{Application.persistentDataPath}/Tips/Tips.txt");
                    }
                }
                GameManager.instance.ChangeScene(loadSceneIndex);
            }
            else
            {
                StartCoroutine(SendVerificationEmail());
            }
        }
        else
        {
            AuthUiManager.instance.LoginScreen();
        }
    }

    private void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if(auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;

            user = auth.CurrentUser;
        }
    }

    public void ClearOutputs()
    {
        loginOutputText.text = "";
        registerOutputText.text = "";
    }

    public void LoginButton()
    {
        StartCoroutine(SetUsernameInDatabase());
        UpdateUserData(0, 0, 10000, 100000, 0, 0);
        StartCoroutine(LoginLogic(loginEmail.text, loginPassword.text));
    }

    public void RegisterButton()
    {
        StartCoroutine(RegisterLogic(registerUsername.text, registerEmail.text, registerPassword.text, registerConfirmPassword.text));
    }

    private IEnumerator LoginLogic(string _email, string _password)
    {
        Credential credential = EmailAuthProvider.GetCredential(_email, _password);

        var loginTask = auth.SignInWithCredentialAsync(credential);

        yield return new WaitUntil(predicate: () => loginTask.IsCompleted);

        if(loginTask.Exception != null)
        {
            FirebaseException firebaseException = (FirebaseException) loginTask.Exception.GetBaseException();
            AuthError error = (AuthError)firebaseException.ErrorCode;
            string output = "Unkown Error, Please Try Again";

            switch (error)
            {
                case AuthError.MissingEmail:
                    output = "Please Enter Your Email";
                    break;
                case AuthError.MissingPassword:
                    output = "Please Enter A Password";
                    break;
                case AuthError.InvalidEmail:
                    output = "Invalid Email";
                    break;
                case AuthError.WrongPassword:
                    output = "Incorrect Password Or Email";
                    break;
                case AuthError.UserNotFound:
                    output = "Incorrect Password Or Email";
                    break;
            }

            loginOutputText.text = output;
        }
        else
        {
            if (user.IsEmailVerified)
            {
                AuthUiManager.instance.loadingScreen();
                if (!Directory.Exists($"{Application.persistentDataPath}/Tips/"))
                {
                    Directory.CreateDirectory($"{Application.persistentDataPath}/Tips/");
                    if (!File.Exists($"{Application.persistentDataPath}/Tips/Tips.txt"))
                    {
                        File.CreateText($"{Application.persistentDataPath}/Tips/Tips.txt");
                    }
                }
                yield return new WaitForSeconds(1f);
                GameManager.instance.ChangeScene(loadSceneIndex);
            }
            else
            {
                StartCoroutine(SendVerificationEmail());
            }
        }
    }

    private IEnumerator RegisterLogic(string _username, string _email, string _password, string _confirmPassword)
    {
        if(_username == "" || _username.ToLower().Contains("cock") || _username.ToLower().Contains("dick") || _username.ToLower().Contains("dickhead") || _username.ToLower().Contains("punani") || _username.ToLower().Contains("pussy") || _username.ToLower().Contains("fuck"))
        {
            registerOutputText.text = "Please Enter A Valid Username";
        }
        else if(_password != _confirmPassword)
        {
            registerOutputText.text = "Passwords Do Not Match";
        }
        else
        {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);

            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

            if(registerTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException) registerTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;
                string output = "Unkown Error, Please Try Again";

                switch (error)
                {
                    case AuthError.InvalidEmail:
                        output = "Invalid Email";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        output = "Email Already In Use";
                        break;
                    case AuthError.MissingPassword:
                        output = "Please Enter A Password";
                        break;
                    case AuthError.WeakPassword:
                        output = "Password Is To Weak";
                        break;
                }

                registerOutputText.text = output;
            }
            else
            {
                UserProfile profile = new UserProfile
                {
                    DisplayName = _username,
                };

                var defaultUserTask = user.UpdateUserProfileAsync(profile);

                yield return new WaitUntil(predicate: () => defaultUserTask.IsCompleted);

                if(defaultUserTask.Exception != null)
                {
                    user.DeleteAsync();
                    FirebaseException firebaseException = (FirebaseException) defaultUserTask.Exception.GetBaseException();
                    AuthError error = (AuthError)firebaseException.ErrorCode;
                    string output = "Unkown Error, Please Try Again";

                    switch (error)
                    {
                        case AuthError.Cancelled:
                            output = "Update User Cancelled";
                            break;
                        case AuthError.SessionExpired:
                            output = "Session Expired";
                            break;
                    }

                    registerOutputText.text = output;
                }
                else
                {
                    StartCoroutine(SendVerificationEmail());
                }
            }
        }
    }

    private IEnumerator SendVerificationEmail()
    {
        if(user != null)
        {
            var emailTask = user.SendEmailVerificationAsync();

            yield return new WaitUntil(predicate: () => emailTask.IsCompleted);

            if(emailTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException) emailTask.Exception.GetBaseException();
                AuthError error = (AuthError) firebaseException.ErrorCode;

                string output = "Unknown Error, Try Again";

                switch (error)
                {
                    case AuthError.Cancelled:
                        output = "Verification Was Cancelled";
                        break;
                    case AuthError.InvalidEmail:
                        output = "Invalid Email";
                        break;
                    case AuthError.TooManyRequests:
                        output = "To Many Requests";
                        break;
                }

                AuthUiManager.instance.AwaitVerification(false, user.Email, output);
            }
            else
            {
                AuthUiManager.instance.AwaitVerification(true, user.Email, null);
            }
        }
    }

    public void UpdateUserData(int level, int experience, int spendableCash, int offshoreAccount, int completedHeists, int playtimeseconds)
    {
        StartCoroutine(UpdateLevel(level));
        StartCoroutine(UpdateExperience(experience));
        StartCoroutine(UpdateSpendableCash(spendableCash));
        StartCoroutine(UpdateOffShoreAccount(offshoreAccount));
        StartCoroutine(UpdateCompletedHeists(completedHeists));
        StartCoroutine(UpdatePlayTimeSeconds(playtimeseconds));
    }

    public void LoadUserDat()
    {
        StartCoroutine(LoadUserData());
    }

    private IEnumerator SetUsernameInDatabase()
    {
        var DBTask = DbRefrence.Child("users").Child(user.UserId).Child("username").SetValueAsync(user.DisplayName);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }

    private IEnumerator UpdateLevel(int level)
    {
        var DBTask = DbRefrence.Child("users").Child(user.UserId).Child("level").SetValueAsync(level);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }

    private IEnumerator UpdateExperience(int experience)
    {
        var DBTask = DbRefrence.Child("users").Child(user.UserId).Child("experience").SetValueAsync(experience);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }

    private IEnumerator UpdateSpendableCash(int spendableCash)
    {
        var DBTask = DbRefrence.Child("users").Child(user.UserId).Child("spendablecash").SetValueAsync(spendableCash);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if(DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }

    private IEnumerator UpdateOffShoreAccount(int offshoreAccount)
    {
        var DBTask = DbRefrence.Child("users").Child(user.UserId).Child("offshoreaccount").SetValueAsync(offshoreAccount);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if(DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }

    private IEnumerator UpdatePlayTimeSeconds(int playtimeSeconds)
    {
        var DBTask = DbRefrence.Child("users").Child(user.UserId).Child("playtimeseconds").SetValueAsync(playtimeSeconds);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }

    private IEnumerator UpdateCompletedHeists(int heists)
    {
        var DBTask = DbRefrence.Child("users").Child(user.UserId).Child("completedHeists").SetValueAsync(heists);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }

    private IEnumerator LoadUserData()
    {
        var DBTask = DbRefrence.Child("users").Child(user.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            LevelHandler.instance.levelSystem.level = 0;
            LevelHandler.instance.levelSystem.experience = 0;
            GameManager.instance.PlayerCurrentSpendableCash = 10000;
            GameManager.instance.PlayerCurrentOffshoreAccount = 100000;
            GameManager.instance.HeistsCompleted = 0;
            GameManager.instance.PlayTimeInHeistsSeconds = 0;
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            int loadedLevel = int.Parse(snapshot.Child("level").Value.ToString());
            LevelHandler.instance.levelSystem.level = loadedLevel;
            LevelHandler.instance.levelSystemAnimated.level = loadedLevel;
            int loadedExperience = int.Parse(snapshot.Child("experience").Value.ToString());
            LevelHandler.instance.levelSystem.experience = loadedExperience;
            LevelHandler.instance.levelSystemAnimated.experience = loadedExperience;

            int.TryParse(snapshot.Child("spendablecash").Value.ToString(), out GameManager.instance.PlayerCurrentSpendableCash);
            int.TryParse(snapshot.Child("offshoreaccount").Value.ToString(), out GameManager.instance.PlayerCurrentOffshoreAccount);
            int.TryParse(snapshot.Child("completedHeists").Value.ToString(), out GameManager.instance.HeistsCompleted);
            int.TryParse(snapshot.Child("playtimeseconds").Value.ToString(), out GameManager.instance.PlayTimeInHeistsSeconds);
        }
    }
}
