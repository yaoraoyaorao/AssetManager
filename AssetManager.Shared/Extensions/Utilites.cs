namespace AssetManager.Shared.Extensions
{
    public static class Utilites
    {
        public static List<string> Icons { get; } = new List<string>
        {
            "Monitor",
            "MicrosoftWindows",
            "AppleIos",
            "Linux",
            "Android",
            "Cloud"
        };

        public static string GetIconName(int id)
        {
            if (id < 0 || id > Icons.Count)
            {
                return "";
            }

            return Icons[id];
        }

        public static string GetIconName(string id)
        {
            return GetIconName(int.Parse(id));
        }

        public static int GetIconId(string name)
        {
            if (Icons.Contains(name))
            {
                return Icons.IndexOf(name);
            }

            return -1;
        }
    }
}
