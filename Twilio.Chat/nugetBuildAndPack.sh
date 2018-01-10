# to be able to build you need installed:
# 1) xamarin itself
# 2) gradle
# 3) xcode and command line tools 
# 4) cocoapods
# 5) sharpie 

# clean
rm *.nupkg

# clean android build
MSBuild Twilio.Chat.Android/Twilio.Chat.Android.csproj /t:Clean /p:Configuration=Debug
MSBuild Twilio.Chat.Android/Twilio.Chat.Android.csproj /t:Clean /p:Configuration=Release
cd Twilio.Chat.Android
gradle clean
rm -rf bin
cd -

# clean ios build
MSBuild Twilio.Chat.iOS/Twilio.Chat.iOS.csproj /t:Clean /p:Configuration=Debug
MSBuild Twilio.Chat.iOS/Twilio.Chat.iOS.csproj /t:Clean /p:Configuration=Release
cd Twilio.Chat.iOS
rm -rf Pods
rm -rf Binding
rm -rf bin
cd -

# fetch artifacts for android 
cd Twilio.Chat.Android
gradle fetch
cd -

# fetch artifacts for ios
cd Twilio.Chat.iOS
pod install
cd -

# build android project
MSBuild Twilio.Chat.Android/Twilio.Chat.Android.csproj  /p:Configuration=Debug
MSBuild Twilio.Chat.Android/Twilio.Chat.Android.csproj  /p:Configuration=Release

# build ios project
cd Twilio.Chat.iOS
sharpie pod bind
cd -
MSBuild Twilio.Chat.iOS/Twilio.Chat.iOS.csproj  /p:Configuration=Debug
MSBuild Twilio.Chat.iOS/Twilio.Chat.iOS.csproj  /p:Configuration=Release

# package for nuget
nuget pack Twilio.Chat.Xamarin.nuspec -Verbosity detailed -Version 0.0.2.0
