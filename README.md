# About
Open Source Non-GPS Location App for UCL Students based on Wifi BSSID data. The app was developed for the Android platform using the Unity engine.

<p align="center">
<img src="https://github.com/alexlaurence/UCLocation/raw/master/prev1.png" data-canonical-src="https://github.com/alexlaurence/UCLocation/raw/master/prev1.png" width="200" height="420"/>
  <img src="https://github.com/alexlaurence/UCLocation/raw/master/prev2.png" data-canonical-src="https://github.com/alexlaurence/UCLocation/raw/master/prev2.png" width="200" height="420"/>
  <img src="https://github.com/alexlaurence/UCLocation/raw/master/prev3.png" data-canonical-src="https://github.com/alexlaurence/UCLocation/raw/master/prev3.png" width="200" height="420"/>
</p>

# Dependencies
- [Android Goodies Pro](https://www.assetstore.unity3d.com/go/v2?from=%23!%2Fcontent%2F67473)
- [CSVReader](https://github.com/tikonen/blog/blob/master/csvreader/CSVReader.cs)
- [64-bit Java Development Kit 8 (1.8)](http://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html)
- [Unity Engine](https://unity3d.com/get-unity/download)
- [Android Environment](https://docs.unity3d.com/Manual/android-sdksetup.html)

# Background
You take the same right seemingly for the fourth time, although you're not sure. You would expect the pulsating pain and the hundred bearings rolling along the floor of your mind to wash out your hungover. "I'll never drink again" you say to yourself as you feel your inside voice sitting in condescending silence. Not being able to keep your head straight has its advantages though, as it makes you catch a glimpse of your watch. It bellows 5 to 9 as you take the same right for the definitely fifth time. You look at your student ID. It doesn't say Theseus. Good. You remember however that you, too, have a thread leading you to freedom (i.e. your 9am, so maybe not really freedom), so you reach into your pocket for it. As you pull your phone out and access maps, a pop-up asks you permission for your high-accuracy location. You want to get out of this mess, so you press agree. After 10 seconds in which the high-accuracy location mode turns your battery percentage into a timer, it accurately plots you in front of a bus on Gower street. You crash on the floor and decide to take a nap.

If this (or a slightly less dramatic version of it) happened to you, we feel you. So we decided to develop an app that can show your location around the UCL campus using not GPS, but the Wi-fi infrastructure of the eduroam network. So the app should theoretically work for anybody connected to the eduroam network (which is basically everybody)

# How it works
We went around campus and recorded in several key locations (more locations are to be added) the MAC address of the Wi-fi to which the phone usually connects. Used when walking around campus, the app constantly reads the MAC Address of the Wi-fi router to which the phone is connected and compares that to our list. If it finds a match, it retrieves the location stored, and gives you the possibility of seeing it on google maps or sending a text to a friend with the room where you are. (You can do this by the use of the two buttons of the app).

| bssid             | room                    | lat      | lon      | 
|-------------------|-------------------------|----------|----------| 
| c4:14:3c:be:52:91 | South Wing              | 51.52424 | -0.13326 | 
| 5c:50:15:91:38:ce | South Junction          | 51.52429 | -0.13317 | 
| 1c:de:a7:40:42:41 | Main Quad               | 51.52436 | -0.13352 | 
| 78:ba:f9:cf:2f:7e | South Cloisters         | 51.5245  | -0.13321 | 
| d0:c7:89:c6:b3:de | Main Quad               | 51.52458 | -0.13402 | 
| 7c:0e:ce:4b:f4:1e | Octagon-South Cloisters | 51.52462 | -0.1334  | 
| 00:e1:6d:0a:aa:ae | Octangon                | 51.52466 | -0.13345 | 
| b0:aa:77:7b:c3:01 | South Cloisters         | 51.52445 | -0.13315 | 
| 28:c7:ce:1e:ao:31 | John Locke              | 51.5248  | -0.13332 | 
| 80:e0:1d:d9:ec:8e | Jeremy Bentham          | 51.52486 | -0.13318 | 
| 80:e0:1d:d9:ec:81 | Jeremy Bentham          | 51.52486 | -0.13318 | 
| b0:aa:77:50:15:3e | Octagon-North Cloisters | 51.52487 | -0.13367 | 
| 78:ba:f9:cf:38:0e | North Cloisters         | 51.52497 | -0.13373 | 
| b0:aa:77:d8:08:0e | North Cloisters         | 51.52501 | -0.1338  | 




# How we built it
We used unity to both create a mock-up to record the mac addresses and for the final product.

# Challenges we ran into
Getting it to work

# Accomplishments that we are proud of
Getting it to work

# What's next for UCLocation
The dream is to make the app show the floor as well. This would be done by remapping the campus taking measurements of the strengths of the signals coming from ALL the wi-fi routers in as many location points a possible. Then, we would be able to differentiate when someone is on one floor or the other, as the strength print would be different (which is something that we cannot do now accurately as the same wi-fi network could be the one "preferred" by the phone at two different levels. One could say that the same thing may happen for our current solution, namely for the same wi-fi to be preferred in more than one room. But from our experience with the app, there is a large enough number of routers around campus for this to not happen).

# Credits
Andrei Parashiv - Ideation, data collection

Alexander Laurence - Backend/Frontend Programming, Unity, UI

Elon Glouberman - Backend Programming, object-orientated pipeline, UX

tikonen - CSVReader
