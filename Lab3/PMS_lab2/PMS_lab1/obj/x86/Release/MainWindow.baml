   M S B A M L   `   `   `  ���� R  NSharpGL.WPF, Version=2.3.0.1, Culture=neutral, PublicKeyToken=27fc851303210b27>.clr-namespace:SharpGL.WPF;assembly=SharpGL.WPFSharpGL.WPF   PMS_lab1   PMS_lab1.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     BsharpGL.clr-namespace:SharpGL.WPF;assembly=SharpGL.WPF       � Title$   ��$	��350��$	��535�� � WindowStartupLocation$ CenterScreen=����#480548.��� .+��  � RowDefinitions �� $	��41* ��� $	��41* ��� $
��108* ��� $
��130* � � ColumnDefinitions �� $��2* ��� $
��0.2* ��� $��1* ��� $
��0.2* �.+�   SharpGL.WPF.OpenGLControl  -   $��openGLControl��   DrawFPS .     RenderContextType$	 FBO=�   	FrameRate$ 20Q�$��4��$��0,0,0.2,-0.2q��� $��2��$��2��$��Stretch=�$	��Top=�.+��� -   $��StartButton��$��Start�����#210021
�������$��18(�$	��100��$��35���� -   $��PauseButton��$��Pause�����#210021
�������$��18(�$	��100��$��35���� -   $��
StopButton��$
��Stop�����#210021
�������$��18(�$	��100��$��35����  �� Orientation$ 
Horizontal=�$��2��$��Stretch=�$	��Top=�$��1��.+��� $��Input a��
�������$��18(��� -   $��InputATextBox�����#210021
�������$��18(� �� TextWrapping$
 Wrap=�$�� ��$��50��$��50���� $ Vertical=�$��2��$��3��$	��Top=�$��Stretch=�.+�	� -   $��LighterRadioButton��	 d� 	IsChecked$
	 TrueY�$��18(�
�������$��Colors to white��$��2��$
��Left=�$��3��$	��Top=�	� -   $��DarkerRadioButton��$��18(�
�������$��Colors to black��$��2��$
��Left=�$��3��$	��Top=�