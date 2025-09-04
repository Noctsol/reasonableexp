# moarexp
Elin game experience mod that spawned from another deprecated one (Experience Multiplier). 



# Notes
First time ever modding anything so will leave this in case anyone picks this up.
This took me longer than I'd like to admit for something so simple.
I NEEDED this mod to take the game reasonable to play. All I wanted was a reasonable exp rate lol.

In the future, the experience functionality might change again. I changed things to be less fragile by patching the method rather than in-line replacements.
What originally broke the original mod was that the ModExp function changed from using an int to a float.

1. Download dns spy
2. Go to your elin installation (mine was at D:\SteamLibrary\steamapps\common\Elin)
3. In dnsspy, open up D:\SteamLibrary\steamapps\common\Elin\Elin_Data\Managed\elin.dll
    - Look for "ModExp"
    - If it doesn't exist, prepare to spend a lot of time digging and writing entirely new code

- write directions
- set up links to original
- attach original


# Guide for running this locally
## Option 1 - Drop it in the package directory
Example: D:\SteamLibrary\steamapps\common\Elin\Package
1. Make a folder called /tempthing in the ..SteamLibrary\steamapps\common\Elin\Package directory
Run `dotnet build -c Release` to make a dll
2. Drop the dll in D:\SteamLibrary\steamapps\common\Elin\BepInEx\plugins
4. Run the game once and a config file will get generated
5. Close the game
6. Edit the file

## Option 2 - Edit the csproj pathing 
1. Edit the csproj pathing to reference your files
2. Run `dotnet build -c Release` to make a dll
3. Drop the dll in D:\SteamLibrary\steamapps\common\Elin\BepInEx\plugins
4. Close the game
5. Edit the file