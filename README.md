# Pokemon-Go-Ban-Check
**CAUTION: Use of this program or anything else third party that uses the unofficial API is a risk for you getting banned. This is intended for developers and for people who have already used some kind of third party tool and/or are suspecting that they are banned.**

Pokemon Go Ban Check (such an original name) is a program to help you determine with ease if your Pokémon Go account is banned. This program requires [.Net Framework 4.6.1](https://www.microsoft.com/en-us/download/details.aspx?id=49981).

The ban wave has affected many players, legit and non legit accounts have been banned. Logic behind the program is if the response is **status_code:2** it means that your account has not been banned, but the response **status_code:3** if your account has been banned. Right now, it is stilll unknown what the ban criteria is so make sure fill out [this](https://goo.gl/forms/tjilsa8OApcjuhj82) form if your account has been banned.


# How to use


###Windows

If you haven't already, download [.Net Framework 4.6.1](https://www.microsoft.com/en-us/download/details.aspx?id=49981). Download one of the [releases](https://github.com/Ra5tko/Pokemon-Go-Ban-Check/releases) and extract the ZIP file. Next, open the directory, hold your left shift and right click in the directory and press "Open command widnow here". Now with the Command Prompt open type in:

```
pogoban -u YourUsername -p YourPassword -t GoogleOrPTC
```

**YourUsername** is the username or email of your account, **YourPassword** is the password of your account and **GoogleOrPTC** is the login provider (your account type which can be either a Google or a Pokémon Trainer Club account), use ```Google``` for a Google login and ```PTC``` for a Pokémon Trainer Club login. Final command should be like:

```
pogoban -u abcd123 -p qwerty -t PTC
```

# Multi-Account Login

Pokemon Go Ban Check supports ban checking for multiple accounts. For every acount you need to specify a username/email and a password and for each one they have to be devided by a single space. You also need to specify the login provider for every account if you are **not** using the -a (--all) argument/option.
```
pogoban -u qwerty abcde ytrewq -p abc123 zxw987 jb007 -t PTC PTC PTC
```
If all of the accounts you are checking use the **same** login provider, you can specify the provider only once by using the **-a** or **--all** option/argument.
```
pogoban -u qwerty abcde ytrewq -p abc123 zxw987 jb007 -t PTC -a
```
If not all of the accounts use the same login provider, then you won't be able to use the -a or --all option/argument.

# Options and multi-account support

All of this arugments can be used together:

####-h, --help
Get a quick list of commands and their explanation.
```
pogoban --help
```

####-u, --username
Username for your Pokémon GO account. For multiple accounts separate each username with a space. **Required.**
```
pogoban --username YourUsername
```


####-p, --password
Password for your Pokémon GO account. For multiple accounts separate each password with a space. **Required.**
```
pogoban --password YourPassword
```

####-t, --provider
Login provider for Pokémon GO, use **Google** for Google login and **PTC** for Pokémon Trainer Club login. **Required.**
```
pogoban --provider Google

```
####-x, --latitude
Specifies the latitude of your login.
```
pogoban -x 0.0
```
```
pogoban --latitude 0.0
```

####-y, --longitude
Specifies the longitude of your login.
```
pogoban -y 0.0
```
```
pogoban --longitude 0.0
```

####-a, --all
Specifies that you will use the same provider with all of the accounts. For more detail look in the Multi-Account Login section.
```
pogoban -a
```
```
pogoban -all
```

#Credits
Thanks to [AeonLucid](https://github.com/AeonLucid) for his [POGOLib](https://github.com/AeonLucid/POGOLib) and to [ChipWolf](https://github.com/ChipWolf) for his incredible feedback.
