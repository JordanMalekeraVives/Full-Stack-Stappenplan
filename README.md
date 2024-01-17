# Full Stack Stappenplan :computer:

Dit is een stappenplan dat je kan volgen voor de examen. Als je fouten ziet gelieve deze te melden.

## 1. Nieuw project aanmaken

1.  Open VS en selecteer `Create a new project`.

2.  Selecteer `ASP.NET Core Web App (Model-View-Controller)`.

    ![ASP.NET Core Web App (Model-View-Controller)](image.png)
    > [!CAUTION]
    > Zorg zerker dat het C# en MVC is.

3.  Vul de projectnaam, locatie in en druk op Next.

4.  Kies voor de juiste framework `.NET 7.0` en authentiecatietype `Invidual accounts`.

    ![Alt text](image-1.png)
    > [!WARNING]
    > Het zou kunnen dat op het examen een andere type of extra dingen gevraagd kunnen worden. Dus kijk goed naar de opgave!

5.  Druk op `Create` om het project te creëren. 

## 2. App architecture opzetten

### A.d.h.v. script

Gebruik deze script om de domains, services en repositories projectmappen aan te maken en ze te toevoegen aan je solution. Het zorgt ook voor de nodige references tuss. de projecten.

1.  Selecteer links boven de optie `View`.

    ![VS Code View](image-2.png)

2.  Selecteer de optie `Terminal`

    ![Terminal](image-3.png)

3.  Onderaan moet je nu een Powershell terminal zien.

    ![PS Terminal](image-4.png)

4. Plak hier de onderstaande code in.

    ```powershell
    # Als je solution naam anders is dan je folder naam,
    # Verander dan $dirName naar de naam van je solution,
    # dus $dirName = "MijnProjectNaam".

    # Variabelen

    $dirName = (Get-Item -Path ".\").Name
    $domainsName = $dirName + ".Domains"
    $servicesName = $dirName + ".Services"
    $reposName = $dirName + ".Repositories"

    # Niewe class libraries toevoegen met de juiste projectnamen.

    dotnet new classlib --name $domainsName
    dotnet new classlib --name $servicesName
    dotnet new classlib --name $reposName

    # De niewe libraries toevoegen aan project sln.

    dotnet sln add $domainsName
    dotnet sln add $servicesName
    dotnet sln add $reposName

    # De juiste references leggen tussen de projecten.

    dotnet add $dirName reference $domainsName
    dotnet add $dirName reference $servicesName
    dotnet add $servicesName reference $domainsName
    dotnet add $servicesName reference $reposName
    dotnet add $reposName reference $domainsName

    # Dit hoeft erbij

    Write-Host "Done"
    ```

5.  Hier zie je dan het resultaat:

    ![App Architectuur](image-5.png)

### Manueel

## 3. Entity Framework opzetten (voor database)

### Packages installeren a.d.h.v. script

Om de packages toe te voegen aan je projecten, kan je de volgende script gebruiken

Plak dit in je Powershell terminal (zie 2. voor terminal tutorial)

```powershell
# Variabelen

$dirName = (Get-Item -Path ".\").Name
$domainsName = $dirName + ".Domains"
$servicesName = $dirName + ".Services"
$reposName = $dirName + ".Repositories"
$packageVersion = "7.0.11"

# SqlServer voor de domains en repos

dotnet add $domainsName package Microsoft.EntityFrameworkCore.SqlServer --version $packageVersion

dotnet add $reposName package Microsoft.EntityFrameworkCore.SqlServer --version $packageVersion

# Tools voor de domains en repos

dotnet add $domainsName package Microsoft.EntityFrameworkCore.Tools --version $packageVersion

dotnet add $reposName package Microsoft.EntityFrameworkCore.Tools --version $packageVersion

# Design voor de domains en views

dotnet add $domainsName package Microsoft.EntityFrameworkCore.Design --version $packageVersion

dotnet add $dirName package Microsoft.EntityFrameworkCore.Design --version $packageVersion

# Onnodige packages removen in views

dotnet remove $dirName package Microsoft.EntityFrameworkCore.SqlServer

dotnet remove $dirName package Microsoft.EntityFrameworkCore.Tools

# Dit hoeft erbij.

Write-Host "Done"