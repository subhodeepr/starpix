   M S B A M L   `   `   `  ����  StarPix    StarPix.SelectPhotos   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Import Photos��5   	   $	��510��6   $	��670��6,    � 
ResizeMode$ CanMinimize=�5   	    � WindowStartupLocation$ CenterOwner=�6"    � Icon$ images/star.ico��6a   .��5      � .+�5   
   � $
��Left=�6   $��78��6*   $��10,0,0,0q�66   $	��Top=�6H   $	��645��6`   .+�5      �� $��Select Photos��6   $��246,10,246,0q�6,   $	��Top=�6B   $��24(�6Z   5	      � 5   
   $
��Left=�6   $	��318��6*   $��507,83,-3,0q�67   $	��Top=�6L   $	��136��6d   .+�5       �� -   5         ��Name$ selectAllButton��5       $��
Select All��6.   $
��Left=�6C   $��
44,138,0,0q�6^   $	��Top=�6r   $��75��6�    � ToolTip$# Select all photos in the view��6�   �� 5!      -   5        $ selectNoneButton��5!      $��Select None��6/   $
��Left=�6E   $��
44,191,0,0q�6`   $	��Top=�6t   $��75��6�   $% Deselect all photos in the view��6�   o� 5"      -   5         o�Name$ collectionsMenu��5"      $
��Left=�6,   $
��Auto��6G   $��
10,248,0,0q�6U   $	��Top=�6i   $
��Auto��6�   .@�5#      m� -   5         m�Name$ collectionList��5#      $��Add To Collection��64   $��20��6O   ���	#FF707070���	#FFDDDDDD$8 2Select or create a new collection to add photos to��6�   .@�5$      m� -   5        $��Add New Collection��5$      6;   �� 5%      	� 5&      -   5        $��
All Photos��5&   "    d� 	IsChecked$
 TrueY�67   6H   5'      5(      5)      � 5*   
   $
��Left=�6   $��44��6*   $��
10,420,0,0q�66   $	��Top=�6J   $	��623��6b   .+�5+      �� -   5        $ importButton��5+      $��Import��6+   $
��Left=�6<   $��
537,10,0,0q�6W   $	��Top=�6k   $��76��6�   {�6�   �� 	 �� ElementName$	 selectListBox��
 �� Path$
 SelectedItems.Count��� 5,      -	   5        $��Cancel��5,      $
��Left=�6&   $��	10,10,0,0q�6A   $	��Top=�6T   $��75��6l   6w   5-      �� 51   
   -
   5        $��selectListBox��51   c   $��Visible=�60    �� SelectionMode$ Extended=�6x   $
��Left=�6�   $	��318��6�   $��	40,78,0,0q�6�   $	��Top=�6�   $	��467��6�   $��Disabled=�6�   ���	#FFE0DEDE$��0q�6=  ��52      �� 53      .R�54      �  � Orientation$ 
Horizontal=�6    $
��Left=�69   55      57      �� 58   
   -   5         ��Name$ noPhotosLabel��58      $��No photos to import��6'   $
��Left=�6E   $��174,211,0,0q�6`   $	��Top=�6u   $��40��6�   $	��184��6�   $��18.667(�6�   $y�Hidden=�6�   5:      5;      