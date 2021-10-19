public static class APIPath
{
  //config
  public const string MainDomain = "http://mydocs.univirlab.com/";
  private const string Prefix = "api/";


  //Auth
  public const string LoginPath = MainDomain + Prefix + "login";
  public const string GetProfilePath = MainDomain + Prefix + "profile";
  public const string LogoutPath = MainDomain + Prefix + "logout";
  public const string RegisterPath = MainDomain + Prefix + "register";
  public const string UpdateProfilePath = MainDomain + Prefix + "profile_update";
}