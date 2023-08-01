# Twilio Chat Xamarin Bindings

NB: For Conversations bindings see this 3rd party package: [jedusei/Twilio.Conversations.Xamarin](https://github.com/jedusei/Twilio.Conversations.Xamarin)

[![NuGet version (Twilio.Chat.Xamarin)](https://img.shields.io/nuget/v/Twilio.Chat.Xamarin.svg)](https://www.nuget.org/packages/Twilio.Chat.Xamarin/)

This repo holds Xamarin project to create Twilio Chat Bindings for Xamarin apps (Android and iOS) and Sample projects for Android and iOS to demonstrate how it can be used. The bindings itself are published ot [NuGet.org](https://www.nuget.org/) with name [Twilio.Chat.Xamarin](https://www.nuget.org/packages/Twilio.Chat.Xamarin/)

## Building component

### Requirements

* Xamarin (Visual Studio Community edition)
* android-sdk 
* xcode (with xcode command-line tools)
* gradle 
* cocoapods

### Building component

```
cd Twilio.Chat
./nugetBuildAndPack.sh
cd -
```
This will clean the project, fetch necessary libs for Android (through Gradle) and iOS (through CocoaPods), build the component and package it to NuGet package.

### Documentation

Where it's possible, the documentation is built into the Xamarin Component. However, it's not full. It's always a good idea to consult with Twilio documentation for respective SDK (iOS or Android): https://www.twilio.com/docs/api/chat/sdks

## Sample app

### Overview

The sample app consists of Xamarin Forms project ChatDemo, shared code ChatDemo.Shared and platform specific projects ChatDemo.iOS and ChatDemo.Droid.

The sample project is fully functional Twilio Chat demo app which works in such way:
* asks for identity and token provider url
* initializes Twilio Chat client with token token provided by token provider for given identity
* fetches all user subscribed channels, gets last 10 messages from channel and gets member count for channels
* subscribes to all events and logs it to the device screen and in the device log
* subsribes for push notifications (FCM for Android and APNS for iOS) and handles the pushes

### Building the sample app

Building the sample project is done from Visual Studio Community Edition for Mac, given that further mentioned prerequisites are met.

#### Common

* configure Token Provider to generate Twilio token for given identity:
> We recommend to host the token provider using [Twilio Runtime Functions](https://www.twilio.com/docs/api/runtime/functions). 
The one should create new Runtime Function and provide necessary values to it. 
[Here](https://gist.github.com/aleksandrsivanovs/abd04d4c139941467ff6b5fa102821e4) one can find sample of Runtime function to generate Twilio Token based on provided `identity` and `pushChannel`.
* change the default token provider url set in [ChatDemoLoginPage.xaml.cs](Sample/ChatDemo/ChatDemoLoginPage.xaml.cs#L13) to Your generated Twilio Runtime Function url

#### Android

* setup the the FCM with help of Firebase console and put the correct [google-services.json](Sample/ChatDemo.Droid/google-services.json) file to the `Sample/ChatDemo.Droid` folder.
* create new credential resource in Twilio Console and setup Token Provider to return this credential sid in Twilio Token for the Android app

#### iOS

* generate certificates and provisioning profile in Apple Developer console and set the Xamarin to use those for the iOS app signing of ChatDemo.iOS project
* create new credential resource in Twilio Console and setup Token Provider to return this credential sid in Twilio Token for the iOS app
