   M S B A M L   `   `   `  ����  StarPix    StarPix.FacebookLogin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Upload photos to Facebook��5   	   $	��300��6+   $	��471��68    � 
ResizeMode$ CanMinimize=�6D    � WindowStartupLocation$ CenterOwner=�6]    � Icon$ images/star.ico��6�   .��5      � .+�5   
   �� -   5         ��Name$ contentControl��5      $��1��61   � 5   
   -   5         �Name$ LogoGrid��5      $
��Left=�6!   $	��117��6<   $��	10,10,0,0q�6I   $	��Top=�6\   $	��443��6t   .+�5      �� $
��Left=�6   $	��100��6/   $��	128,7,0,0q�6<   $	��Top=�6O   $	��209��6g   $ ��images/facebookLogoBig.png��6s   �� 5	      -   5         ��Name$ 
errorLabel��5	      $
��Left=�6(   $��25,107,0,-16q�6C   $	��Top=�6Y   $	��391��6q   $��Center=�6}   $y�	Collapsed=�6�   ���	#FFFF0202��5
      �� 5       �� 
TargetType" ��6    �� Triggers 5      P� 5      	 P� Property	 � y�
 P� Value$
 Visible=�6<    N� EnterActions 5   "   �� 5   &   .��5   *   �� .��5   .   W�  �� TargetProperty$ Opacity�6>    g� Duration$ 00:00:01D�6b    g� 	BeginTime$ 00:00:00X�6v    W� From$	 0.0X�6�   	 W� To$	 1.0X�6�   W� 5   .   $ Opacity�6>   $ 00:00:01D�6b   $ 00:00:03X�6v   $	 1.0X�6�   $	 0.0X�6�   5   +   5   '   5      5      .��5      '%Please enter an email and/or password5      5      � 5   
   -   5        $ 	LoginGrid��5      $
��Left=�6"   $	��127��6=   $��
10,132,0,0q�6J   $	��Top=�6^   $	��443��6v   .+�5      �� -   5         ��Name$ CancelButton��5      $��Cancel��6+   $
��Left=�6<   $��	10,97,0,0q�6W   $	��Top=�6j   $��75��6�   $��5��6�   �� 5       -   5        $ LoginButton��5       $��Log In��6*   $
��Left=�6;   $��
358,97,0,0q�6V   $	��Top=�6j   $��75��6�   $��4��6�   �� 5!      -   5        $ PasswordLabel��5!      $��	Password:��6+   $
��Left=�6?   $��	96,37,0,0q�6Z   $	��Top=�6m   G� 5"      -   5         G�Name$ PasswordTextbox��5"      $��23��63   $��161,39,135,0q�6?   $	��Top=�6U   $��2��6m   6z   �� 5#      -	   5        $ 
EmailLabel��5#      $��Email:��6(   $
��Left=�69   $��
117,10,0,0q�6T   $	��Top=�6h   �� 5$      -
   5         ��Name$ EmailTextbox��5$      $��23��6,   $��161,13,135,0q�68   $	��Top=�6N   $��1��6f   6s   �� 5%      -   5         ��Name$ saveCredentials��5%      $��Save Log-in Info��60   $
��Left=�6K   $��
177,67,0,0q�6f   $	��Top=�6z   $��3��6�   5&      5'      5(      