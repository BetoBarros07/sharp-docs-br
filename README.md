# Sharp Docs Brazilian

[![Build](https://travis-ci.org/BetoBarros07/sharp_docs_br.svg)](https://travis-ci.org/BetoBarros07/sharp_docs_br)
[![Nuget Version](http://img.shields.io/nuget/v/O7.SharpDocsBR.svg)](http://www.nuget.org/packages/O7.SharpDocsBR)
[![Issues open](https://img.shields.io/github/issues/betobarros07/sharp_docs_br.svg)](https://github.com/BetoBarros07/sharp_docs_br/issues)
[![Unlicense](https://img.shields.io/badge/license-unlicense-orange.svg)](LICENSE)

Sharp Docs Brazilian is a library with some DataAnnotations to validate brazilian documents in ASP.NET.

## Download Package

This is very simple, you just need download the package from nuget:

###  Command Line

* Dotnet CLI    : `dotnet add package O7.SharpDocsBR`
* Visual Studio : `Install-Package O7.SharpDocsBR`

### Adding reference in .csproj file

The first step that you will need to do is implement the following code in your .csproj file:

```xml
<ItemGroup>
  <PackageReference Include="O7.SharpDocsBR" Version="THE_VERSION_HERE" />
</ItemGroup>
```

After that, you will execute the following command to restore the package from the nuget using Dotnet CLI:

`dotnet restore`

If you are in Visual Studio, just build the project and the IDE will do the magic!

## How to use

### Parameters

First you need to know, that those documents have a mask, and in all the cases we have a parameter to ignore these masks:

* `ExcludeNonNumericCharacters:` this exclude the alphanumeric characters that make part of the mask, the default value is false, if you want to change set this to true.

To use this lib you will just add this as an attribute on your model field:

```c#
using O7.SharpDocsBR;

namespace Foo
{
    public class BarModel
    {
        [CPFValidation(ExcludeNonNumericCharacters = true)]
        public string CPF { get; set; }

        [CNPJValidation(ExcludeNonNumericCharacters = true)]
        public string CNPJ { get; set; }
    }
}
```

An observation is that I passed the `ExcludeNonNumericCharacters` to true, but by default is false, I change because in this case my model will receive those documents with the mask from the front-end.

## License

[UNLICENSE](LICENSE) © [Beto Barros](https://github.com/betobarros07)