   M S B A M L   `   `   `  ����  StarPix    StarPix.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  StarPix��5   	   $	��768��6   $��900         ��6&    � 
ResizeMode$ CanMinimize=�5       � WindowStartupLocation$ CenterScreen=�6$    � Icon$ images/star.ico��6n   .��5      � .+�5   
   �� -   5         ��Name$ dragSelectionCanvas��5      $y�	Collapsed=�5	      .+�5      �� -   5         ��Name$ dragSelectionBorder��5      
����  �$��1q�5      
����ح� �� CornerRadius$ 1��5       J� Opacity$	 0.5Q�5      5      o� 5   
   $
��Left=�6   $��25��6*   $��57,0,0,0q�66   $	��Top=�6H   $	��120��6`   .@�5      m� -   5         m�Name$ manageLibrary��5      $��14(�6.   $��Manage Library��6<   $��Center=�6T   $��Segoe UI Symbol)�6v   $��25��6�   $	��120��6�   .@�5      m� -   5        $ importPhotos��5      $��Import Photos��61   	 � ToolTip$0	 *Import photos from the latest image source��6c   m� 5      -   5        $ manageCollections��5      $��Manage Collections��66   $)	 #Add, rename, and delete collections��6r   5      5      �� 5   
   -   5        
 ��Name$
 	statusBar��5      $��32��6'   $��
0,708,0,-1q�63   $	��Top=�6G   .@�5      �� -   5         ��Name$ statusTextWrapper��5      $��32��67   $	��Top=�6C   $
��Left=�6[   .��5      �� -	   5         ��Name$ photoStatus��5      $��Italic$�61   $��13(�6D   $��Light"�6R   $	��561��6e   $��33��6q   5      �� 5      -
   5        $ photoSizeSliderWrapper��5      $��24��6<   $	��Top=�6H   $��Right=�6`   .��5       �� -   5         ��Name$ photoSizeSlider��5       $��94��62    � Value$ 10Q�6=    �� IsSnapToTickEnabled .  � Minimum$ 0Q�6c    � Maximum$ 10Q�6o    �� TickFrequency$ 5Q�6|   $D	 >Drag left to decrease image size, right to increase image size��6�   5!      5"      �� 5#   
   -   5         ��Name$ 	searchBar��5#      $
��Left=�6%   $��25��6@   $��	202,0,0,0q�6L   $��Search photos by tags��6_   $	��Top=�6|   $	��566��6�   $��Center=�6�   $��Italic$�6�   
������� �� 	IsEnabled . $��0,0,20,0q�6A  6T  �� 5$   
   $
��Left=�6   $��15��6+   $��	744,6,0,0q�67   $	��Top=�6J   $��17��6b   $ ��images/magnifyingglass.png��6m   � 5%   
   $
��Left=�6   $��29��6*   $��	40,46,0,0q�66   $	��Top=�6I   $	��787��6a    � RowDefinitions 5&      �� 5'      $	��10* �6    �� 5(      $	��19* �6    .+�5*      �� -   5         ��Name$ successLabel��5*      $��
129,0,70,0q�6*   $	��Top=�6>   $��Center=�6V   $y�	Collapsed=�6{   ���	#FF0E9539$��29��6�   $��2��6�   ��5+      �� 5,       �� 
TargetType" ��6    �� Triggers 5-      P� 5.       P� Property � y� P� Value$ Visible=�6<    N� EnterActions 5/   "   �� 50   &   .��51   *   �� .��52   .   W�  �� TargetProperty$ Opacity�6>    g� Duration$ 00:00:01D�6b    g� 	BeginTime$ 00:00:00X�6v    W� From$	 0.0X�6�   	  W� To$	  1.0X�6�   W� 53   .   $ Opacity�6>   $ 00:00:01D�6b   $ 00:00:04X�6v   $	 1.0X�6�   $	  0.0X�6�   54   +   55   '   57      59      5;      o� 5<      -   5        ! o�Name$
! sort��5<      $
��Auto��6!   $��	741,3,0,0q�6/   $	��Top=�6B   $��2��6Z   .@�5=      m� $
��Sort��6   $��20��6)   $��46��65   $��Center=�6@   $��Center=�6]   $2	 ,Select a sorting option for the current view��6�   .@�5>      	� -   5        " 	�Name$" 
sortByDate��5>   "   $��By Date��66   # 	� 	GroupName$
# Sort��6H   $ d� 	IsChecked$
$ TrueY�6Y   6j   	� 5?      -   5        $" sortByFileType��5?   "   $��By File Type��6:   $
# Sort��6Q   6b   	� 5@      -   5        $" 
sortByName��5@   "   $��By Name��66   $
# Sort��6H   6Y   �� 5A      	� 5B      -   5        $" sortAscending��5B   "   $��	Ascending��69   $# By��6M   $
$ TrueY�6\   6m   	� 5C      -   5        $" sortDescending��5C   "   $��
Descending��6:   $# By��6O   6^   5D      5E      o� 5F      -   5        $! collectionsMenu��5F      $
��Left=�6,   $
��Auto��6G   $��19,3,0,0q�6U   $	��Top=�6g   $��81��6   $��2��6�   .@�5G      m� -   5        $ collectionsList��5G      $��Collections��64   $��20��6I   $��82��6U   $!	 Select a collection to view��6`   .@�5H      	� -   5        $��
All Photos��5H   "   $
$ TrueY�67   6H   5I      5J      5K      � 5M   
   $
��Left=�6   $��25��6*   $��	792,0,0,0q�66   $	��Top=�6I   $��64��6a   .+�5N      �� -   5        
������ $��Right=�5N   .   $��0,0,30,0q�6J   $	��Top=�6\   $��24��6t   $��25��6   $��0q�6�   {�6�   �� % �� ElementName$% _listbox��& �� Path$& SelectedItems.Count���5O      �� 5P      .R�5Q      �� ' ��Name$
' img1��6    $
��Fill=�6.   $ ��images/facebook_button.png��6=   $��0q�6a   $*	 $Upload selected photo(s) to Facebook��6l   ( ^� ShowOnDisabled( . 6�   ) �� Triggers) 5R      P� 5S       � {� .  . �5T   "   �� * �� 
TargetName$
* img1��6U   + �� Property+ � ��, �� Value$, images/fb-bw2.png��6;   6i   �� 5U   "   + �@^�ToolTip$., (Please select photos to use this feature��6K   6~   5V      5X      5Z      5[      �� 5]   
   -   5        $' trashCan��5]      $
��Left=�6"   $��38��6=   $��838,647,0,0q�6I   $	��Top=�6^   $��40��6v   $0��*images/trash_recyclebin_empty_closed_w.png��6�   - J� 	AllowDrop- . $*	 $Drag photos over trash bin to delete��6�   5^      �� 5_   
   -   5        $��_listbox��5_   ^   $��Visible=�6+   . �� SelectionMode$. Extended=�6n   $	��591��6�   $��
57,94,58,0q�6�   $	��Top=�6�   $��Disabled=�6�   ���	#FFE0DEDE$��0q�6  ���	#FFC4CD11��5`      �� 5a      .R�5b      � / � Orientation$/ 
Horizontal=�6    $��Center=�69   5c      5e      �� 5f   
   -   5        $ noEntryLabel��5f      $y�	Collapsed=�6&   $��18(�6=   $&�� No photos found with that search��6K   $��308,353,220,311q�6v   �� 5g   
   -   5        $ noPhotosLabel��5g      $y�	Collapsed=�6'   $��18(�6>   $��No photos to display��6L   $��359,353,300,311q�6k   5h      5i      