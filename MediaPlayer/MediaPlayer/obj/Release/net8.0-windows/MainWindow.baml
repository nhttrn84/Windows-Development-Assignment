   M S B A M L   `   `   `  ���� F  BMediaPlayer, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MediaPlayerMediaPlayer      MediaPlayer.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:MediaPlayer     -     � Title$  Media Player��$	��500��$	��900�� J� CommandBindings )  #System.Windows.Input.CommandBinding  -      Command$ ApplicationCommands.Open��  -   $ MediaCommands.Play��  -   $ MediaCommands.Pause��  -   $ MediaCommands.Stop��.���  � RowDefinitions �� $
��Auto ��� $	��80* ��� $
��Auto ��� $
��Auto �.+�c� $��0��.@��� $��ApplicationCommands.Open��.���� .+��� $��/images/folder.png��$��20��$��20�� �� BitmapScalingMode$ HighQuality=��� $��Choose File���� �� -   .���� .+��� $��/images/load.png��$��20��$��20��$ HighQuality=��� $��Load PLaylist���� �� -   .���� .+��� $��/images/save.png��$��20��$��20��$ HighQuality=��� $��Save Playlist���� �� -   .���� .+��� $��/images/delete.png��$��20��$��20��$ HighQuality=��� $��Remove Item���� $��1�� �� Orientation$ 
Horizontal=�.+��  � ColumnDefinitions �� $	��18* ��� $	��80* ��� $��0��$	��100��
�������.+��� -	    ��Name$ 	mediaList��$
��Left=����� .R��� ���� *
FileName+r� -
   $��mePlayer��$��1�� r� LoadedBehavior$ Manual=�	 r� Stretch$	 Uniform=��� -   
 ��Name$
 
imgPreview��$��1��$	��100��$	��100��$��Center=�$��Center=�$y�Hidden=��� $��2������ .R��  �� $
��Auto ��� $��* ��� $
��Auto �.@��� .���� -   $��lblProgressStatus��
00:00:00�� $��1��$��Stretch=�.���� -   $��sliProgress�� �� IsMoveToPointEnabled$
 True���� $��2��.���� -   $��	sliVolume��$	��100��$��12�� � Value$	 0.5Q� � Maximum$ 1Q�$
 True���� $��3��$ 
Horizontal=�$��Center=�
��� �|�.+��� -   .���� $��/images/back.png��$��20��$��20��$ HighQuality=��� $��MediaCommands.Play��.���� $��/images/play.png��$��20��$��20��$ HighQuality=��� $��MediaCommands.Pause��.���� $��/images/pause.png��$��20��$��20��$ HighQuality=��� $��MediaCommands.Stop��.���� $��/images/stop.png��$��20��$��20��$ HighQuality=��� -   .���� $��/images/skip.png��$��20��$��20��$ HighQuality=��� -   .���� -   $
 shuffle��$��/images/non_shuffle.png��$��20��$��20��$ HighQuality=�