using System.Collections.Generic;
using DSInternals.Win32.WebAuthn.COSE;

namespace DSInternals.Win32.WebAuthn.PasskeyUI
{
    public interface IAlgorithmSelectorViewModel
    {
        List<Algorithm> SelectedAlgorithms { get; set; }
    }
}
