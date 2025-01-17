namespace ResXManager.VSIX.Compatibility
{
    using System.Globalization;

    using Microsoft.VisualStudio;

    public static class ItemKind
    {
        public const string CSharpProject = "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}";
        public const string SolutionFolder = "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}";
        public const string SolutionFile = "{66A26722-8FB5-11D2-AA7E-00C04F688DDE}";
        public static readonly string PhysicalFile = VSConstants.GUID_ItemType_PhysicalFile.ToString("B", CultureInfo.InvariantCulture);
    }
}