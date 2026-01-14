---
external help file: DSInternals.Passkeys-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/New-PasskeyRandomChallenge.md
schema: 2.0.0
---

# New-PasskeyRandomChallenge

## SYNOPSIS
Generates a random challenge to be used by WebAuthn.

## SYNTAX

```
New-PasskeyRandomChallenge [[-Length] <Int32>] [<CommonParameters>]
```

## DESCRIPTION
{{ Fill in the Description }}

## EXAMPLES

### EXAMPLE 1
```
New-PasskeyRandomChallenge -Length 32
```

Generates a random 32-byte challenge.

## PARAMETERS

### -Length
The length of the challenge in bytes.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
Default value: 32
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### System.Byte[]
## NOTES

## RELATED LINKS
