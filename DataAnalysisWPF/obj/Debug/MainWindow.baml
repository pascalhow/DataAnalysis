   M S B A M L   `   `   `  ����J  FDataAnalysisWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:DataAnalysisWPFDataAnalysisWPF  5   	        DataAnalysisWPF.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:DataAnalysisWPF     5   	     � Title$  Data Analysis��5   	   $��524.226��6   $��516.155��60    � 
ResizeMode$ NoResize=�6@    � WindowStartupLocation$ CenterScreen=�6V   .��5
      � .+�5   
   �� -   5         ��Name$ directory_txtBox��5      $
��Left=�6,   $��23��6G   $��	10,35,0,0q�6S    �� TextWrapping$
 Wrap=�6f   $	��Top=�6z   $	��385��6�   �� 5   
   -   5         ��Name$ lbl_directory��5      $��	Directory��6'   $
��Left=�6;   $��	10,10,0,0q�6V   $	��Top=�6i   �� 5   
   -   5         ��Name$ btn_openFile��5      $
��Open��6'   $
��Left=�66   $��
414,35,0,0q�6Q   $	��Top=�6e   $��75��6}   6�   �� 5   
   -   5        $ btn_analyseFile��5      $��A n a l y s e��6*   $
��Left=�6B   $��	10,78,0,0q�6]   $	��Top=�6p   $	��479��6�   6�   7  1System.Windows.Forms.Integration.WindowsFormsHost  5   
   -   5          Name$ windowsFormsHost��5      $
��Left=�65   $	��236��6P   $��
10,124,0,0q�6]   $	��Top=�6q   $	��479��6�      
Background �	#FFB9B9B9	   
Foreground
	 �   �6�   �� 5   
   -   5        $ lbl_mean��5      $��Mean:��6"   $
��Left=�62   $��	9,382,0,0q�6M   $	��Top=�6`   $��16(�6x   $
��Bold"�6�   �� 5   
   -   5        $ lbl_popStdDev��5      $$��Population Standard Deviation:��6'   $
��Left=�6P   $��
10,413,0,0q�6k   $	��Top=�6   $��16(�6�   $
��Bold"�6�   �� 5   
   -   5        $ lbl_sampleStdDev��5      $ ��Sample Standard Deviation:��6*   $
��Left=�6O   $��
10,444,0,0q�6j   $	��Top=�6~   $��16(�6�   $
��Bold"�6�   5      5      