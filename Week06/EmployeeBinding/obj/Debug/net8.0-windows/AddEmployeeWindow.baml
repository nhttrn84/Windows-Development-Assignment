   M S B A M L   `   `   `  ����J  FEmployeeBinding, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:EmployeeBindingEmployeeBinding  5   	   '    !EmployeeBinding.AddEmployeeWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:EmployeeBinding     5   	     � Title$  AddEmployeeWindow��5   	   $	��450��6#   $	��800��60   .��5	      �� .+�5
   
   �� $*��$Vui lòng nhập các thông tin sau��6    �� Left$	 316��6;   
 �� Top$ 54��6M   �� 5   
   $��Họ và Tên: ��6   $	 144��6'   $	 112��69   �� 5   
   -   5         ��Name$ fullNameTextBox��5      $	��150��6+   $��25��67   $	 221��6C   $	 107��6U   $
��Left=�6f   $��Center=�6�   �� 5   
   $��Email: ��6   $	 144��6#   $	 151��65   $��Center=�6F   $	��Top=�6c   �� 5   
   -   5        $ emailTextBox��5      $	��150��6(   $��25��64   $	 221��6@   $	 146��6R   $
��Left=�6c   $��Center=�6~   �� 5   
   $��Địa chỉ: ��6   $	 417��6%   $	 112��67   $
��Left=�6H   $��Center=�6c   �� 5   
   -   5        $ addressTextBox��5      $	��200��6*   $��25��66   $	 500��6B   $	 107��6T   $
��Left=�6e   $��Center=�6�   �� 5   
   $��Số điện thoại: ��6   $	 417��6+   $	 151��6=   �� 5   
   -   5        $ phoneNumberTextBox��5      $	��100��6.   $��25��6:   $	 500��6F   $	 146��6X   $
��Left=�6i   $	��Top=�6�   �� 5   
   $#��Đường dẫn hình ảnh: ��6   $	 144��60   $	 193��6B   $��Center=�6S   $	��Top=�6p   �� 5   
   -   5        $ avatarPathTextBox��5      $	��200��6-   $��25��69   $	 300��6E   $	 188��6W   $
��Left=�6h   $��Center=�6�   �� 5   
   -   5        $��Thêm��5      $��50��6    $��25��6+   $	 300��6W   $	 240��6i   $
��Left=�6z   $��Center=�6�   �� 5   
   -   5        $��Hủy��5      $��50��6   $��25��6*   $	 450��6Q   $	 240��6c   5      5      