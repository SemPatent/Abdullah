   M S B A M L   `   `   `  ����   Practicals_2014)    #Practicals_2014.Pages.PatentsFromDB   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Z VFirstFloor.ModernUI, Version=1.0.5.0, Culture=neutral, PublicKeyToken=2d21ec3cd074c59a0mui&http://firstfloorsoftware.com/ModernUI  X TXceed.Wpf.Toolkit, Version=2.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  $��481.133��$	��886��.���    ContentRoot#��[  .+��� $��
0,-10,0,10q�.���� $	��200��.+��� $=��7Process English and Russian Patents From Local Database��  Heading2#��[ $��18(��� -     ��Name$  
DBLocation��$��Database Location��$
��Left=�$��
0,15,20,20q�$	��150��$��30���� -   $  SaveLocation��$��Save Location��$
��Left=�$��
0,30,20,20q�$	��150��$��30���� -   $  	GetPatent��$��Get Extracts��$
��Left=�$��0,140,20,20q�$	��150��$��30�� J� RenderTransformOrigin$ 0.5,0.5&��� -    ��Name$ saveLocation��$��label��$��30��$	��854��$
��Left=�$��0,146,0,261.333q�$ 0.499,0.467&��� -   $ 
dbLocation��$ ��Patents Database Location:��$��30��$	��854��$
��Left=�$��0,63,0,344.333q�$ 0.499,0.467&�� -    �Name$ pbStatus��$
��Left=�$��28��$��	0,377,0,0q�$	��Top=�$	��844�� � Minimum$ 0Q� � Maximum$	 100Q� � BindingGroup &   System.Windows.Data.BindingGroup  �� $��Center=�$	��Top=�$��	0,377,0,0q�����  �� ElementName$ pbStatus�� �� Path$ Value�	 �� StringFormat$	 {0:0}%���� -   $ StatusLabel��$
��Left=�$��	0,352,0,0q�$	��Top=�$	��844��$��20��.���� $��Ready��