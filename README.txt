=================================================
Human Computer Interactions Design Project
StarPix - A Photo Management Solution
Students: Subhodeep Ray-Chaudhuri, Behnam Nikkhah
=================================================

This application was written using C# programming language and WPF framework.

Steps:
----------------------------------
1. Extract the folder StarPix to a desired location

NOTE: This application requires MS Access Database to run properly. In order to run the application
you must have MS Access installed or install both the following for your system (either x64 or x86 will work):

- 2007 Office System Driver: Data Connectivity Components: https://www.microsoft.com/en-ca/download/details.aspx?id=13255

- Microsoft Access Database Engine 2010 Redistributable: https://www.microsoft.com/en-ca/download/details.aspx?id=13255

------------------------------------------------------------------------------------------------------------------------
2. Run the application under StarPix -> Bin -> Debug -> StarPix.exe

*if the application does not start, the database may be corrupted or you may need to re-instantiate the database.*
	- Delete StarPix.accdb.
	- Launch the StarPix.sln project into Visual Studio and click "Start" under Debug. 
	  *It may take a few seconds for the application to rebuild the database of photos.*
	- You can continue to use the application as normal, or close Visual Studio and run StarPix.exe from the steps above. 
------------------------------------------------------------------------------------------------------------------------

Notes/Possible Issues

- Photos are stored as relative links into the database, so photo names cannot have spaces in between them. 
- If you want to add more photos for import, you can drag photos into the "new_photos" directory.
- Import photos may be slow depending on how many photos are being added. This function is not multithreaded with the progress bar, so it may not correctly reflect the actual progress.
- Pressing Cancel on import or upload progress bar does not pause the background worker. The cancel will only take effect if the confirmation is done before the progress bar reaches 100%.
- To import or "Upload to Facebook", you must have at least one photo selected. Else this function will be greyed out.
- To rename or delete a collection, you must have at least one collection selected. Else this function will be greyed out.
- To select multiple images on the main screen, you must use keyboard shortcuts such as CTRL + A, or CTRL + click, or SHIFT + click on photos. 
- To perform multi operations on images, select a group of photos and right click anywhere on those photos to bring up the context menu. 
- Facebook upload button is a stub. It does not actually upload photos to Facebook.
- To search, you must press Enter after typing a keyword of a tagged image. It is a wildcard search of every tag in the database.
- Only medium to large size inputs (1-1000 characters) have been tested for text fields. The application may possibly crash if extremely large inputs are given. 
- All visible elements should have working functionality.


