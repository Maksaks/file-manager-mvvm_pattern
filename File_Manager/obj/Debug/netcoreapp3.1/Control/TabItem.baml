   M S B A M L   `   `   `  ????G  CFile_Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:File_Manager.ControlFile_Manager.Control  5      +clr-namespace:File_ManagerFile_Manager  5	      ?$clr-namespace:File_Manager.ViewModelFile_Manager.ViewModel  5
      "    File_Manager.Control.TabItem   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:File_Manager.Control     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a7behav+http://schemas.microsoft.com/xaml/behaviors  5      a ]GongSolutions.WPF.DragDrop, Version=3.0.0.0, Culture=neutral, PublicKeyToken=91f1945125b7a587ddurn:gong-wpf-dragdrop  5      #uiclr-namespace:File_Manager  5	      4	viewmodel$clr-namespace:File_Manager.ViewModel  5
        ? 	Resources  5      1  +File_Manager.Control.ConverterObjectToImage   ImgConverter??%!   &	          5   
   5      5      .??5      ?  ? RowDefinitions 5   
   ?? 5      $??* ?6    ? ColumnDefinitions 5   
   ?? 5      $	??300 ?6   ?? 5      $??* ?6   .+?5   
   ?? $??0??6   
???????
???   ?$??1 0 1 1q?6G   ._?5      ?? 
???????$??10q?6,   $	??300??6^   ??68   ?? *SelectObjInfo+??5      ?? 5      .R?5      ?? 
???   ?$??0 0 0 1q?65   ._?5      ?? $
??Wrap=?6:    ?? Padding$ 5q?6N   $??10q?6Z   $??20(?6f   $	??250??6t   $??DemiBold"?6?   ??6(   ?? 6?   5      5       5"      5$      ?? 5(   
   -   5        $??listMain??5(      $??1??6"   
???????$??Center=?5-      $??Center=?5.      $??Disabled=?5/      )  #GongSolutions.Wpf.DragDrop.DragDrop   IsDragSource$
 True??51         IsDropTarget$
 True??52         File_Manager.DragDropCl   instance   DropHandler# Z 53         UseDefaultDragAdorner$
 True??54      #	   UseDefaultEffectDataTemplate$
	 True??55      
 ? DataContext
 5)      ?? *.+??5*      ?? *
CurrentDir+ ?? SelectedItem 5+      ?? *
SelectedOb+ ? ContextMenu 56      ?? 57      .@?58      m? $??Відкрити??6    m? Command 61   ?? *GoINCommand+ m? CommandParameter 6Q   ?? *
SelectedOb+m? 59      $2??,Відкрити нову вкладинку??6    6@   ?? *parent.AddNewTabItemCommand+m? 5:      $??Оновити??6    60   ?? *ReloadCommand+m? 5;      $0??*Повернутися на головну??6    6?   ?? *GoHomeCommand+m? 5<      $??Вставити??6    61   ?? *parent.PasteCommand+m? 5=      $0??*Створити новий каталог??6    6?   ?? *parent.AddFolderCommand+m? 5>      $??Фільтр??6   .@?5?      m? -   5         m?Name$ docxx??5?   #   $??Файли .docx??62   
???   ?$ Файли .docx??6?    6[   ?? *FillterCollectionCommand+5A      m? 5B      -   5        $	 pyy??5B   #   $??Файли .py??60   
???   ?$ Файли .py??6?    6V   ?? *FillterCollectionCommand+5D      m? 5E      -   5        $	 css??5E   #   $??Файли .cs??60   
???   ?$ Файли .cs??6?    6W   ?? *FillterCollectionCommand+5G      ?? 5H      
???   ?$??0 1 0 0q?65   ._?5I      m? -   5        $ uuser??5I   '   $"??Користувацький??66   
???   ?$" Користувацький??6?    6b   ?? *FillterCollectionCommand+5K      5L      5M      m? 5N      $??Сортування??6   .@?5O      m? -   5        $
 time??5O   #   $??За часом??61   
???   ?$ За часом??6?    6W   ?? *SortCollectionCommand+5Q      m? 5R      -   5        $
 type??5R   #   $??За типом??61   
???   ?$ За типом??6?    6W   ?? *SortCollectionCommand+5T      m? 5U      -   5        $
 name??5U   #   $??За ім'ям??61   
???   ?$ За ім'ям??6?    6W   ?? *SortCollectionCommand+5W      5X      m? 5Y      $??Обраний файл??6   {?65   ?? *
IsSelected+.@?5Z      m? $??Копіювати??6#    66   ?? *parent.CopyCommand+m? 5[      $??Вирізати??6#    65   ?? *parent.CutCommand+m? 5\      $??Вставити??6#    65   ?? *parent.PasteCommand+m? 5]      $??Видалити??6#    65   ?? *parent.DeleteCommand+m? 5^      $??Змінити ім'я??6#    69   ?? *RenameCommand+5_      5a      *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5c      +  %Microsoft.Xaml.Behaviors.EventTrigger  5d         	EventName$ MouseDoubleClick??6%   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5e      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction  -	   5        GoInCom??   Command 5f   %   ?? *GoINCommand+   CommandParameter 5g   %   ??  ?? ElementName$ listMain?? ?? Path$ SelectedItem?5h      ??5j      ?? 5k      .R?5l      ? 5n      5o      ??5r      ?? 5s       ?? 
TargetType" ??6   .?5t      ??  ?? Property ?@J?SnapsToDevicePixels ?? Value$
 True??6<   ?? 5u       ? ??$ 4q?60   ?? 5v       ? ?? 6C   ?? *HorizontalContentAlignment+ ?? RelativeSource ??  ?? AncestorType" ???? 5w       ? ?? 6A   ?? *VerticalContentAlignment+ ?? " ???? 5x       ? ??
 ?????63   ?? 5y       ? ??
 ?????64   ?? 5z       ? ??$ 1q?68   ?? 5{       ? ??$	 150??6.   ?? 5|       ? ??$	 150??6/   ?? 5}       ? ?? 5~      ?? 5       ?? 
TargetType" ??6.   .R?5?   "   ? .+?5?   &   ??  ??Name$ Bd??6-    ?? CornerRadius$ 10??5?   )   #??z??5?   )   #??z??5?   )   #??z??5?   )    ?? Padding# z??5?   )     J? SnapsToDevicePixels$
  true??5?   )   $??10q?5?   )   ._?5?   &   ?? $??1??67   #??z??6D   #  z  6?   #??z??6?   5?   #   5?   #   ! ?? Triggers! 5?   "   ]? 5?   &   " ]? 
Conditions" 5?   *   ?? 5?   .   # ?? Property# ?@J?IsMouseOver$ ?? Value$
$ True??6O   ./?5?   *   ?? % ?? 
TargetName$% Bd??61    ? ??
 ?????5?   +   5?   '   ]? 5?   &   " 5?   *   ?? 5?   .   # ?@??IsSelectionActive$$ False??6^   ?? 5?   .   # ?@??
IsSelected$
$ True??6N   ./?5?   *   ?? $% Bd??61    ? ??
 ?????6f   5?   '   ]? 5?   &   " 5?   *   ?? 5?   .   # ?@??IsSelectionActive$
$ True??6^   ?? 5?   .   # ?@??
IsSelected$
$ True??6N   ./?5?   *   ?? $% Bd??61    ? ??
 ?????6j   5?   '   P? 5?   &   & P? Property& ? {?' P? Value$' False??6C   . ?5?   *   ?? $% Bd??6S   # ? ??6c    ? ??5?   '   5?      5?      5?      ??5?      ?? 5?      .R?5?      ? 
??????  5?      ?? 5?      ?? 5?      $
??Auto ?6,   .+?5?      ?? ??6    ?? *.+( ?? 	Converter#( [  ?? 5?      
???   ?$??1??67   $??15(?6Z   $??CharacterEllipsis=?6h   $??Center=?6?   ??6D   ?? *NAME+6?   5?      5?      5?      5?      5?      