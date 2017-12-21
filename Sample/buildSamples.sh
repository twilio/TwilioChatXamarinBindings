#clean ChatDemo.Droid 
MSBuild ChatDemo.Droid/ChatDemo.Droid.csproj /t:Clean /p:Configuration=Debug
MSBuild ChatDemo.Droid/ChatDemo.Droid.csproj /t:Clean /p:Configuration=Release
rm -rf ChatDemo.Droid/bin

#build  ChatDemo.Droid
MSBuild ChatDemo.Droid/ChatDemo.Droid.csproj  /p:Configuration=Debug
MSBuild ChatDemo.Droid/ChatDemo.Droid.csproj  /p:Configuration=Release
MSBuild ChatDemo.Droid/ChatDemo.Droid.csproj  /t:SignAndroidPackage /p:Configuration=Release /p:AndroidApplication=True

#clean ChatDemo.iOS 
rm -rf ChatDemo.iOS/bin
MSBuild ChatDemo.iOS/ChatDemo.iOS.csproj /t:Clean /p:Configuration=Debug
MSBuild ChatDemo.iOS/ChatDemo.iOS.csproj /t:Clean /p:Configuration=Release

#build ChatDemo.iOS 
MSBuild ChatDemo.iOS/ChatDemo.iOS.csproj  /p:Configuration=Debug
MSBuild ChatDemo.iOS/ChatDemo.iOS.csproj  /p:Configuration=Release