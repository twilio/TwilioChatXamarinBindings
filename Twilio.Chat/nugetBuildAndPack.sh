#!/bin/bash
# to be able to build you need installed:
# 1) xamarin itself
# 2) gradle
# 3) xcode and command line tools 
# 4) cocoapods
# 5) sharpie 

# Exit on errors
set -e

# clean
rm -f *.nupkg

setupAndroid() {
    # clean android build

    MSBuild Twilio.Chat.Android/Twilio.Chat.Android.csproj /t:Clean /p:Configuration=Debug
    MSBuild Twilio.Chat.Android/Twilio.Chat.Android.csproj /t:Clean /p:Configuration=Release

    cd Twilio.Chat.Android

    gradle clean
    rm -rf bin

    # fetch artifacts for android 
    gradle fetch

    cd -

    # build android project

    MSBuild Twilio.Chat.Android/Twilio.Chat.Android.csproj  /p:Configuration=Debug
    MSBuild Twilio.Chat.Android/Twilio.Chat.Android.csproj  /p:Configuration=Release
}

setupIos() {
    # clean ios build

    MSBuild Twilio.Chat.iOS/Twilio.Chat.iOS.csproj /t:Clean /p:Configuration=Debug
    MSBuild Twilio.Chat.iOS/Twilio.Chat.iOS.csproj /t:Clean /p:Configuration=Release

    cd Twilio.Chat.iOS

    rm -rf Pods
    rm -rf Binding
    rm -rf bin

    # fetch artifacts for ios
    pod repo update
    pod install

    # build ios project

    sharpie pod bind # This only binds the framework
    # To update generated bindings, run `sharpie bind -f Pods/TwilioChatClient/TwilioChatClient.framework` manually.

    cd -

    MSBuild Twilio.Chat.iOS/Twilio.Chat.iOS.csproj  /p:Configuration=Debug
    MSBuild Twilio.Chat.iOS/Twilio.Chat.iOS.csproj  /p:Configuration=Release
}

setupAndroid
setupIos

# package for nuget
nuget pack Twilio.Chat.Xamarin.nuspec -Verbosity detailed
