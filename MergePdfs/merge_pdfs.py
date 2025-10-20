import os
import tkinter as tk
from tkinter import filedialog, messagebox
from PyPDF2 import PdfMerger


def merge_pdfs_in_directory(folder_path, output_filename="合并后的PDF文件.pdf"):
    merger = PdfMerger()
    pdf_files = [f for f in os.listdir(folder_path) if f.lower().endswith(".pdf")]
    pdf_files.sort()

    if not pdf_files:
        raise FileNotFoundError("该目录中没有 PDF 文件。")

    for pdf in pdf_files:
        path = os.path.join(folder_path, pdf)
        merger.append(path)

    output_path = os.path.join(folder_path, output_filename)
    merger.write(output_path)
    merger.close()
    return output_path


def select_folder():
    folder_selected = filedialog.askdirectory(title="请选择包含 PDF 文件的文件夹")
    if folder_selected:
        folder_path.set(folder_selected)


def start_merge():
    folder = folder_path.get().strip()
    if not folder:
        messagebox.showwarning("提示", "请先选择一个文件夹。")
        return

    try:
        output_file = merge_pdfs_in_directory(folder)
        messagebox.showinfo("成功", f"PDF 合并完成！\n\n输出文件：\n{output_file}")
    except FileNotFoundError as e:
        messagebox.showerror("错误", str(e))
    except Exception as e:
        messagebox.showerror("错误", f"发生错误：{e}")


# --- 图形界面部分 ---
root = tk.Tk()
root.title("PDF 合并工具")
root.geometry("480x200")
root.resizable(False, False)

folder_path = tk.StringVar()

tk.Label(root, text="请选择 PDF 文件所在文件夹：", font=("Microsoft YaHei", 11)).pack(pady=10)

frame = tk.Frame(root)
frame.pack(pady=5)

entry = tk.Entry(frame, textvariable=folder_path, width=45)
entry.pack(side=tk.LEFT, padx=5)

browse_btn = tk.Button(frame, text="浏览...", command=select_folder)
browse_btn.pack(side=tk.LEFT)

merge_btn = tk.Button(root, text="开始合并", command=start_merge, bg="#4CAF50", fg="white", font=("Microsoft YaHei", 12), width=15)
merge_btn.pack(pady=20)

tk.Label(root, text="© 2025 Dignite Tools", fg="gray").pack(side=tk.BOTTOM, pady=5)

root.mainloop()
