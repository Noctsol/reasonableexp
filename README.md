# moarexp
Elin game experience mod that spawned from another deprecated one (Experience Multiplier). 
The original code is at https://github.com/hvits3rk/ElinExperienceMultiplier. I was just named "ExperienceMultiplier"

First time ever modding anything so will leave this in case anyone picks this up.
This took me longer than I'd like to admit for something so simple.
I NEEDED this mod to take the game reasonable to play. All I wanted was a reasonable exp rate lol.


# Locating Game Files
In the future, the experience functionality might change again. I changed things to be less fragile by patching the method rather than in-line replacements.
What originally broke the original mod was that the ModExp function changed from using an int to a float.

1. Download dns spy
2. Go to your elin installation (mine was at D:\SteamLibrary\steamapps\common\Elin)
3. In dnsspy, open up D:\SteamLibrary\steamapps\common\Elin\Elin_Data\Managed\elin.dll
    - Look for "ModExp"
    - If it doesn't exist, prepare to spend a lot of time digging and writing entirely new code



# Guide for running this locally
1. You are going to need to edit the .csproj \<ElinDir\> with your actual game installation
2. Run `dotnet build -c Release` to make a dll
3. It will generate a file under obj\Release\net472\noctsol.reasonableExp.dll
2. Drop the dll in D:\SteamLibrary\steamapps\common\Elin\BepInEx\plugins
4. Run the game once and a config file will get generated
5. Close the game
6. Edit the file

