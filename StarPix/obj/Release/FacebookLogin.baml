   M S B A M L   `   `   `  ����   StarPix    StarPix.FacebookLogin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Upload photos to Facebook��$	��300��$	��471�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterOwner=�.��� .+��� -    ��Name$ contentControl��$��1��� -    �Name$ LogoGrid��$
��Left=�$	��117��$��	10,10,0,0q�$	��Top=�$	��443��.+��� $
��Left=�$	��100��$��	128,7,0,0q�$	��Top=�$	��209��$ ��images/facebookLogoBig.png���� -    ��Name$ 
errorLabel��$
��Left=�$��25,107,0,-16q�$	��Top=�$	��391��$��Center=�$y�	Collapsed=����	#FFFF0202����  �� 
TargetType" �� �� Triggers P�  P� Property � y�	 P� Value$	 Visible=�
 N� EnterActions
 �� .���� .��W�  �� TargetProperty$ Opacity� g� Duration$ 00:00:01D� g� 	BeginTime$ 00:00:00X� W� From$	 0.0X�	 W� To$	 1.0X�W� $ Opacity�$ 00:00:01D�$ 00:00:03X�$	 1.0X�$	 0.0X�.�� Please enter valid credentials� -   $ 	LoginGrid��$
��Left=�$	��127��$��
10,132,0,0q�$	��Top=�$	��443��.+��� -    ��Name$ CancelButton��$��Cancel��$
��Left=�$��	10,97,0,0q�$	��Top=�$��75��$��5���� -   $ LoginButton��$��Login��$
��Left=�$��
358,97,0,0q�$	��Top=�$��75��$��4���� -   $ PasswordLabel��$��	Password:��$
��Left=�$��	96,37,0,0q�$	��Top=�G� -    G�Name$ PasswordTextbox��$��23��$��161,39,135,0q�$	��Top=�$��2���� -	   $ 
EmailLabel��$��Email:��$
��Left=�$��
117,10,0,0q�$	��Top=��� -
    ��Name$ EmailTextbox��$��23��$��161,13,135,0q�$	��Top=�$��1���� -    ��Name$ saveCredentials��$��Save Credentials��$
��Left=�$��
177,67,0,0q�$	��Top=�$��3��