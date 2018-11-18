# UCLocation
Open Source Location App for UCL Students based on Wifi BSSID.

# Dependencies
- [Android Goodies Pro](https://www.assetstore.unity3d.com/go/v2?from=%23!%2Fcontent%2F67473)

<p align="center">
<img src="https://github.com/alexlaurence/UCLocation/blob/master/prev.png" data-canonical-src="https://github.com/alexlaurence/UCLocation/blob/master/prev.png" width="200" height="400"/>
</p>

# Background
You take the same right seemingly for the fourth time, although you're not sure. You would expect the pulsating pain and the hundred bearings rolling along the floor of your mind to wash out your hungover. "I'll never drink again" you say to yourself as you feel your inside voice sitting in condescending silence. Not being able to keep your head straight has its advantages though, as it makes you catch a glimpse of your watch. It bellows 5 to 9 as you take the same right for the definitely fifth time. You look at your student ID. It doesn't say Theseus. Good. You remember however that you, too, have a thread leading you to freedom (i.e. your 9am, so maybe not really freedom), so you reach into your pocket for it. As you pull your phone out and access maps, a pop-up asks you permission for your high-accuracy location. You want to get out of this mess, so you press agree. After 10 seconds in which the high-accuracy location mode turns your battery percentage into a timer, it accurately plots you in front of a bus on Gower street. You crash on the floor and decide to take a nap.

If this (or a slightly less dramatic version of it) happened to you, we feel you. So we decided to develop an app that can show your location around the UCL campus using not GPS, but the Wi-fi infrastructure of the eduroam network. So the app should theoretically work for anybody connected to the eduroam network (which is basically everybody)

# How it works
We went around campus and recorded in several key locations (more locations are to be added) the MAC address of the Wi-fi to which the phone usually connects. Used when walking around campus, the app constantly reads the MAC Address of the Wi-fi router to which the phone is connected and compares that to our list. If it finds a match, it retrieves the location stored, and gives you the possibility of seeing it on google maps or sending a text to a friend with the room where you are. (You can do this by the use of the two buttons of the app).

# How we built it
We used unity to both create a mock-up to record the mac addresses and for the final product.

# Challenges we ran into
Getting it to work

# Accomplishments that we are proud of
Getting it to work

# What's next for UCLocation
The dream is to make the app show the floor as well. This would be done by remapping the campus taking measurements of the strengths of the signals coming from ALL the wi-fi routers in as many location points a possible. Then, we would be able to differentiate when someone is on one floor or the other, as the strength print would be different (which is something that we cannot do now accurately as the same wi-fi network could be the one "preferred" by the phone at two different levels. One could say that the same thing may happen for our current solution, namely for the same wi-fi to be preferred in more than one room. But from our experience with the app, there is a large enough number of routers around campus for this to not happen).
