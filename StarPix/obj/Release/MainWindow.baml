   M S B A M L   `   `   `  ����   StarPix    StarPix.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  StarPix��$	��510��$	��706�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=�.��� .+��� -    ��Name$ dragSelectionCanvas��$y�	Collapsed=�.+��� -    ��Name$ dragSelectionBorder��
����  �$��1q�
����ح� �� CornerRadius$ 1�� J� Opacity$	 0.5Q�o� $
��Left=�$��25��$��40,0,0,0q�$	��Top=�$	��120��.@�m� -    m�Name$ manageLibrary��$��14(�$��Manage Library��$��Center=�$��Segoe UI Symbol)�$��25��$	��120��.@�m� -   $ importPhotos��$��Import Photos��m� -   $ manageCollections��$��Manage Collections���� -    ��Name$ 	statusBar��$��32��$��0,450,0,-0.5q�$	��Top=�.@��� -   	 ��Name$	 statusTextWrapper��$��32��$	��Top=�$
��Left=�.���� -   
 ��Name$
 photoStatus��$��Italic$�$��13(�$��Light"�$	��561��$��33���� -	   $	 photoSizeSliderWrapper��$��24��$	��Top=�$��Right=�.���� -
    ��Name$ photoSizeSlider��$��94�� � Value$ 10Q� �� IsSnapToTickEnabled .  � Minimum$ 0Q� � Maximum$ 10Q� �� TickFrequency$ 5Q��� -    ��Name$ 	searchBar��$
��Left=�$��25��$��	175,0,0,0q�$��Search photos by tags��$	��Top=�$	��391��$��Center=�$��Italic$�
������� �� 	IsEnabled . $��0,0,20,0q��� $
��Left=�$��25��$	��Top=�$��26��$��images/star.png��$��6,0,0,0q��� $
��Left=�$��15��$��	544,6,0,0q�$	��Top=�$��17��$ ��images/magnifyingglass.png��� $
��Left=�$��29��$��	40,46,0,0q�$	��Top=�$	��595��.+�o� -    o�Name$
 sort��$
��Auto��$��	558,3,0,0q�$	��Top=�.@�m� $
��Sort��$��20��$��39��$��Center=�$��Center=�.@�	� -    	�Name$ 
sortByDate��$��By Date�� 	� 	GroupName$
 Sort�� d� 	IsChecked$
 TrueY�	� -   $ 
sortByName��$��By Name��$
 Sort��	� -   $ sortByFileType��$��By File Type��$
 Sort���� 	� -   $ sortAscending��$��	Ascending��$ By��$
 TrueY�	� -   $ sortDescending��$��
Descending��$ By��o� -   $ collectionsMenu��$
��Left=�$
��Auto��$��0,3,0,0q�$	��Top=�$
��Auto��.@�m� -   $ collectionsList��$��Collections��$��20��$��82��.@�	� -   $��
All Photos��$
 TrueY�� $
��Left=�$��25��$��	571,0,0,0q�$	��Top=�$��64��.+��� -   
������ $
��Left=�$��10,0,0,0q�$	��Top=�$��24��$��25��$��0q�.���� -    ��Name$
 img1��$
��Fill=�$ ��images/facebook_button.png��$��0q��� ���	#FFDCDADA$��1q�$
��Left=�$	��319��$	��Top=�$	��595��$��	40,87,0,0q�._��� -    ��Name$ sortingLabel��$
��2015��$��Center=�$��
-1,-1,18,0q�$	��Top=�$��Medium"�$	��576��$��Italic$�$��14(�$��Center=�$��30���� -   $ trashCan��$
��Left=�$��38��$��649,398,0,0q�$	��Top=�$��41��$0��*images/trash_recyclebin_empty_closed_w.png�� J� 	AllowDrop .  � ToolTip$* $Drag photos over trash bin to delete������  �� 
TargetType" �� �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ���  �� Property �@J�Effect  �� Value  3  -System.Windows.Media.Effects.DropShadowEffect  !   ShadowDepth$! 0Q�"   Color$" Yellow��#   Opacity$# 1Q�$   
BlurRadius$$ 5Q��� -   $��_listbox��$��Visible=�% �� SelectionMode$% Extended=�$
��Left=�$	��289��$��
40,117,0,0q�$	��Top=�$	��595��$��Disabled=����	#FFE0DEDE$��0q����	#FFC4CD11���� .R�� & � Orientation$& 
Horizontal=�$
��Left=��� -   $ noEntryLabel��$y�	Collapsed=�$��18(�$&�� No photos found with that search��$
��Left=�$��196,252,0,0q�$	��Top=��� -   $ noPhotosLabel��$y�	Collapsed=�$��18(�$��No photos to display��$��244,252,0,0q�$	��Top=�$
��Left=�$	��176��