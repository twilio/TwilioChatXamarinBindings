# How to update to a newer Twilio SDKs version

Steps to update Xamarin bindings for the new version of Android and/or iOS SDKs.

## Configure Xamarin builds on new machine

* `brew install fastlane`
* `brew cask install visual-studio xamarin-ios xamarin-android objectivesharpie dotnet homebrew/cask-versions/adoptopenjdk8`
* Android SDK requires OpenJDK8 – that's why we've installed it above.
* Set env variables
  * `JavaSdkHome` = `$adoptopenjdk8_HOME` = `/Library/Java/JavaVirtualMachines/adoptopenjdk-8.jdk/Contents/Home/`
  * `AndroidSdkHome` = `$ANDROID_HOME`
* Login to new shell to have env vars active.

## For each new SDK update do the following

* Update Twilio SDK versions in dependencies. See [this commit](https://github.com/twilio/TwilioChatXamarinBindings/commit/ac23edbadd55953e9ba8ea42c6dbbc277fb1e81e) for a list of places that need to be updated.
* Run `Twilio.Chat/nugetBuildAndPack.sh` for the first time. It will most certainly fail and in the next steps we will fix it:
* In `/Applications/Xcode.app/Contents/Developer/Platform/iPhoneOS.platform/Developer/SDKs/` set up a symlink with the version that `sharpie` compains about. It usually looks like `"iphoneos13.4 sdk is required but not installed. You might need to update Xcode"`. Just take a symlink with iOS SDK version LARGER than what it asks for and make a copy with appropriate version number, that will shut it up.
* Run `sharpie bind -f Pods/TwilioChatClient/TwilioChatClient.framework` from the `Twilio.Chat/Twilio.Chat.iOS` folder. This will update the iOS bindings. It usually generates some unnecessary garbage - clean it up.
* Run `Twilio.Chat/nugetBuildAndPack.sh` again. This should now succeed and generate you a nuget package.
* Consume it via the demo app solution in Visual Studio - you can run both ios and android simulators from the studio, and play around with the app.
* You will most probably want to update `Sample/ChatDemo/ChatDemoLoginPage.xaml.cs` near line 13 to include your token generator link:
  * `this.tokenProviderUrlEntry.Text = "https://your-server.com/tokenGeneratorUrl";`

## Publishing the package

* Log in to `nuget.org` using MS Live password.
* Click `Upload` in the top menu bar, drag the nuget package you generated and validate all the information is correct:
  * [Version is updated](https://github.com/twilio/TwilioChatXamarinBindings/commit/1f5a8b5f2c9e32525029a7d8ca187b60564f6cf9)
  * Release Notes are correct for this release
  * The package has actually been tested with iOS and Android demo app in both Release and Debug modes at least on Simulator.
* Press Submit and wait a few minutes.

## Tricky parts

* Android bindings are generated using XML remapping files, `Metadata.xml`, `EnumFields.xml` and `EnumMethods.xml` – this is largely empyrical, trial-and-error and is pretty badly documented. See past commits for an idea what could be done. Validate with the demo app that your changes actually work - it could compile and then just crash at runtime with no backtrace, so don't do many large changes at once.
* iOS Bindings are usually easier to process because they are pre-generated and you could modify them to your liking afterwards - see `ApiDefinitions.cs` and `StructsAndEnums.cs`. Conversion tool will generate `[Verify]` annotations that break compilation but in most cases can be just safely deleted. Read compiler output on those - it's usually helpful.

## Useful links

* [Xamarin JAR Binding Library - Metadata.xml tips for missing abstract method — Xamarin Community Forums][1]
* [Android Java Binding - Change Method Signature?! — Xamarin Community Forums][2]
* [Sharing Common Code Between Android and iOS Using Xamarin | James Lavery][3]
* [xamarin - Partial Declaration must not specify different Base Classes - Stack Overflow][4]
* [Java integration with Xamarin.Android - Xamarin | Microsoft Docs][5]
* [Java Bindings Metadata - Xamarin | Microsoft Docs][6]
* [Customizing Bindings - Xamarin | Microsoft Docs][7]
* [Mono for Android - Java Binding enum type to C# in XAMARIN][8]
* [xamarin.android - Xamarin Android Mapping Java.Lang.Enum to C# Enum - Stack Overflow][9]
* [xamarin-android/enum-conversion-mappings.xml at master · xamarin/xamarin-android][10]
* [Binding an .AAR - Xamarin | Microsoft Docs][11]
* Android Internals [Architecture - Xamarin | Microsoft Docs][12]
* [Xamarin.iOS errors - Xamarin | Microsoft Docs][13]
* [Linking Xamarin.iOS Apps - Xamarin | Microsoft Docs][14]


[1]: https://forums.xamarin.com/discussion/35713/xamarin-jar-binding-library-metadata-xml-tips-for-missing-abstract-method
[2]: https://forums.xamarin.com/discussion/31668/android-java-binding-change-method-signature
[3]: https://jglavery.wordpress.com/2013/10/06/sharing-common-code-between-android-and-ios-using-xamarin/
[4]: https://stackoverflow.com/questions/37177154/partial-declaration-must-not-specify-different-base-classes
[5]: https://docs.microsoft.com/en-us/xamarin/android/platform/java-integration/
[6]: https://docs.microsoft.com/en-us/xamarin/android/platform/binding-java-library/customizing-bindings/java-bindings-metadata
[7]: https://docs.microsoft.com/en-us/xamarin/android/platform/binding-java-library/customizing-bindings/
[8]: http://mono-for-android.1047100.n5.nabble.com/Java-Binding-enum-type-to-C-in-XAMARIN-td5713478.html
[9]: https://stackoverflow.com/questions/36919688/xamarin-android-mapping-java-lang-enum-to-c-sharp-enum
[10]: https://github.com/xamarin/xamarin-android/blob/master/build-tools/enumification-helpers/enum-conversion-mappings.xml
[11]: https://docs.microsoft.com/en-us/xamarin/android/platform/binding-java-library/binding-an-aar
[12]: https://docs.microsoft.com/en-us/xamarin/android/internals/architecture
[13]: https://docs.microsoft.com/en-us/xamarin/ios/troubleshooting/mtouch-errors#MT5202
[14]: https://docs.microsoft.com/en-us/xamarin/ios/deploy-test/linker
