   M S B A M L   `   `   `  ����  StarPix    StarPix.RenameCollection   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Rename Collection��5   	   $	��170��6#   $	��300��60    � WindowStartupLocation$ CenterOwner=�6<    � Icon$ images/star.ico��6`   .��5      � .+�5   
   � $
��Left=�6   $	��112��6*   $��	28,14,0,0q�67   $	��Top=�6J   $	��240��6b   .+�5      �� $��Enter Collection Name��6   $��
44,-5,45,0q�64   $	��Top=�6H   $��14(�6`   $��SemiBold"�6n   $��34��6�   �� 5      -   5         ��Name$ 
errorLabel��5      $
��Left=�6(   $��0,24,0,0q�6C   $	��Top=�6U   $	��240��6m   $��Center=�6y   $y�	Collapsed=�6�   ���	#FFFF0202��5	      �� 5
       �� 
TargetType" ��6    �� Triggers 5      P� 5       P� Property � y� P� Value$ Visible=�6<    N� EnterActions 5   "   �� 5   &   .��5   *   �� .��5   .   W� 	 �� TargetProperty$	 Opacity�6>   
 g� Duration$
 00:00:01D�6b    g� 	BeginTime$ 00:00:00X�6v    W� From$	 0.0X�6�   	 W� To$	 1.0X�6�   W� 5   .   $	 Opacity�6>   $
 00:00:01D�6b   $ 00:00:03X�6v   $	 1.0X�6�   $	 0.0X�6�   5   +   5   '   5      5      5      �� 5      -   5         ��Name$ collectionName��5      $��	0,52,0,38q�6.   6A   �� 5      -   5         ��Name$ cancelButton��5      $��Cancel��6+   $
��Left=�6<   $��0,92,0,0q�6W   $	��Top=�6i   $��75��6�   6�   �� 5      -   5        $ okButton��5      $��Ok��6'   $
��Left=�64   $��
165,92,0,0q�6O   $	��Top=�6c   $��75��6{   6�   5      5      5      