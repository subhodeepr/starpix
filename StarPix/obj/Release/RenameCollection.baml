   M S B A M L   `   `   `  ����   StarPix    StarPix.RenameCollection   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Rename Collection��$	��170��$	��300�� � WindowStartupLocation$ CenterOwner=�.��� .+�� $
��Left=�$	��112��$��	28,14,0,0q�$	��Top=�$	��240��.+��� $��Enter Collection Name��$��
44,-5,45,0q�$	��Top=�$��14(�$��SemiBold"�$��34���� -    ��Name$ 
errorLabel��$
��Left=�$��0,24,0,0q�$	��Top=�$	��240��$��Center=�$y�	Collapsed=����	#FFFF0202����  �� 
TargetType" �� �� Triggers P�  P� Property � y� P� Value$ Visible=� N� EnterActions �� .���� .��W�  �� TargetProperty$ Opacity�	 g� Duration$	 00:00:01D�
 g� 	BeginTime$
 00:00:00X� W� From$	 0.0X�	 W� To$	 1.0X�W� $ Opacity�$	 00:00:01D�$
 00:00:03X�$	 1.0X�$	 0.0X�.��,*A collection with that name already exists�� -    ��Name$ collectionName��$��	0,52,0,38q��� -    ��Name$ cancelButton��$��Cancel��$
��Left=�$��0,92,0,0q�$	��Top=�$��75���� -   $ okButton��$��Ok��$
��Left=�$��
165,92,0,0q�$	��Top=�$��75��