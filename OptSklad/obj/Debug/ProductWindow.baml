   M S B A M L   `   `   `  ����C  ?OptSklad, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:OptSkladOptSklad  5   	       OptSklad.ProductWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:OptSklad     5   	     � Title$  ProductWindow��5   	   $	��450��6   $	��800��6,   .��5	      � .+�5
   
   ��  �� Orientation$ Vertical=�6   .+�5      �� $ 
Horizontal=�6   $
��0,10q�62   .+�5      �  � 
ItemHeight$ 30��6    � Orientation$ Vertical=�6,   $
��10,0q�6C   .+�5      �� $��Тип продукта��6   �� 5      ��6   �� *ProdTypeList+ �� SelectedItem 6D   �� *CurrentProduct.ProductType+��5      �� 5      .R�5   "   �� ��6(   �� *Title+5      5      5      � 5      $ 30��6   $ Vertical=�6,   $
��10,0q�6C   .+�5      �� $��Наименование��6   �� 5      $��18(�6D   ��6   �� *CurrentProduct.Title+6D   5      5      �� 5      $ 
Horizontal=�6   $
��0,10q�62   .+�5      �� $��Артикул��6   �� 5      $��50��6H   $��0,0,10,0q�6V   ��6   �� *CurrentProduct.ArticleNumber+6V   �� 5      $��Описание��6   �� 5      $��50��6F   $��0,0,10,0q�6T   ��6   �� *CurrentProduct.Description+6T   �� 5       $��Цена��6   �� 5!      $��50��6?   $��0,0,10,0q�6M   ��6   �� *CurrentProduct.Cost+6M   5"      �� 5#      -   5         ��Name$ 
SaveButton��5#      $��Сохранить��6)   $
��Left=�6V   �� 5$      -   5        $ DeleteButton��5$      $��Удалить��6+   $
��Left=�6X   5%      5&      5'      