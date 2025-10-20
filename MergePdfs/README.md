## 安装以下两个python依赖库:
pip install PyPDF2
pip install pyinstaller

## 打包命令:
pyinstaller --onefile --noconsole --icon=dignite.ico merge_pdfs.py

## 参数说明：

--onefile ：打包成单一的 .exe 文件

--noconsole ：不显示命令行窗口（只显示GUI）

--icon=pdf.ico ：可选，给程序添加一个图标（如果没有图标文件可以去掉这项）
