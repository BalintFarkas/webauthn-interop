namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// The kind of package signature for an authenticator plugin.
    /// </summary>
    public enum PackageSignatureKind : uint
    {
        /// <summary>
        /// No signature.
        /// </summary>
        None = 0,

        /// <summary>
        /// Developer signed package.
        /// </summary>
        Developer = 1,

        /// <summary>
        /// Enterprise signed package.
        /// </summary>
        Enterprise = 2,

        /// <summary>
        /// Store signed package.
        /// </summary>
        Store = 3,

        /// <summary>
        /// System signed package.
        /// </summary>
        System = 4
    }
}
