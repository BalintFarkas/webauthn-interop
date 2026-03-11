using System.Collections.Generic;
using DSInternals.Win32.WebAuthn.COSE;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal sealed class AlgorithmSelectorViewModel : BindableBase, IAlgorithmSelectorViewModel
{
    public AlgorithmSelectorViewModel()
    {
        // Configure default values
        SelectDefaultAlgorithms();
    }

    public List<Algorithm> SelectedAlgorithms
    {
        get
        {
            // Convert checkboxes to PubKeyCredParam

            var result = new List<Algorithm>();

            if (AlgorithmES256Enabled)
                result.Add(Algorithm.ES256);

            if (AlgorithmES384Enabled)
                result.Add(Algorithm.ES384);

            if (AlgorithmES512Enabled)
                result.Add(Algorithm.ES512);

            if (AlgorithmRS256Enabled)
                result.Add(Algorithm.RS256);

            if (AlgorithmRS384Enabled)
                result.Add(Algorithm.RS384);

            if (AlgorithmRS512Enabled)
                result.Add(Algorithm.RS512);

            if (AlgorithmPS256Enabled)
                result.Add(Algorithm.PS256);

            if (AlgorithmPS384Enabled)
                result.Add(Algorithm.PS384);

            if (AlgorithmPS512Enabled)
                result.Add(Algorithm.PS512);

            if (AlgorithmEdDSAEnabled)
                result.Add(Algorithm.EdDSA);

            return result;
        }
        set
        {
            // Convert PubKeyCredParam to checkboxes
            if (value == null)
            {
                SelectDefaultAlgorithms();
                return;
            }

            ClearSelectedAlgorithms();

            foreach (var algorithm in value)
            {
                switch (algorithm)
                {
                    case Algorithm.ES256:
                        AlgorithmES256Enabled = true;
                        break;
                    case Algorithm.ES384:
                        AlgorithmES384Enabled = true;
                        break;
                    case Algorithm.ES512:
                        AlgorithmES512Enabled = true;
                        break;
                    case Algorithm.RS256:
                        AlgorithmRS256Enabled = true;
                        break;
                    case Algorithm.RS384:
                        AlgorithmRS384Enabled = true;
                        break;
                    case Algorithm.RS512:
                        AlgorithmRS512Enabled = true;
                        break;
                    case Algorithm.PS256:
                        AlgorithmPS256Enabled = true;
                        break;
                    case Algorithm.PS384:
                        AlgorithmPS384Enabled = true;
                        break;
                    case Algorithm.PS512:
                        AlgorithmPS512Enabled = true;
                        break;
                    case Algorithm.EdDSA:
                        AlgorithmEdDSAEnabled = true;
                        break;
                }
            }
        }
    }

    public bool AlgorithmRS512Enabled
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                RaisePropertyChanged(nameof(SelectedAlgorithms));
            }
        }
    }

    public bool AlgorithmRS384Enabled
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                RaisePropertyChanged(nameof(SelectedAlgorithms));
            }
        }
    }

    public bool AlgorithmRS256Enabled
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                RaisePropertyChanged(nameof(SelectedAlgorithms));
            }
        }
    }

    public bool AlgorithmPS512Enabled
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                RaisePropertyChanged(nameof(SelectedAlgorithms));
            }
        }
    }

    public bool AlgorithmPS384Enabled
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                RaisePropertyChanged(nameof(SelectedAlgorithms));
            }
        }
    }

    public bool AlgorithmPS256Enabled
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                RaisePropertyChanged(nameof(SelectedAlgorithms));
            }
        }
    }

    public bool AlgorithmES512Enabled
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                RaisePropertyChanged(nameof(SelectedAlgorithms));
            }
        }
    }

    public bool AlgorithmES384Enabled
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                RaisePropertyChanged(nameof(SelectedAlgorithms));
            }
        }
    }

    public bool AlgorithmES256Enabled
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                RaisePropertyChanged(nameof(SelectedAlgorithms));
            }
        }
    }

    public bool AlgorithmEdDSAEnabled
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                RaisePropertyChanged(nameof(SelectedAlgorithms));
            }
        }
    }

    private void ClearSelectedAlgorithms()
    {
        AlgorithmES256Enabled = false;
        AlgorithmES384Enabled = false;
        AlgorithmES512Enabled = false;
        AlgorithmPS256Enabled = false;
        AlgorithmPS384Enabled = false;
        AlgorithmPS512Enabled = false;
        AlgorithmRS256Enabled = false;
        AlgorithmRS384Enabled = false;
        AlgorithmRS512Enabled = false;
        AlgorithmEdDSAEnabled = false;
    }

    private void SelectDefaultAlgorithms()
    {
        ClearSelectedAlgorithms();
        AlgorithmRS256Enabled = true;
        AlgorithmES256Enabled = true;
        AlgorithmEdDSAEnabled = true;
    }
}
