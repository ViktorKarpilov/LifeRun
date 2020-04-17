import tkinter as tk
from tkinter import ttk


class Main(tk.Frame):
    def __init__(self, root):
        super().__init__(root)
        self.init_main()

    def init_main(self):
        toolbar = tk.Frame(bg='#d7d8e0', bd=2)
        toolbar.pack(side=tk.TOP, fill=tk.X)

        self.add_img = tk.PhotoImage(file='')
        btn_open_dialog = tk.Button(toolbar, text='Регистрация', command=self.open_dialog, bg='#d7d8e0', bd=0, compound=tk.TOP, image=self.add_img)

        btn_open_dialog.pack(side=tk.LEFT)

        self.tree = ttk.Treeview(self, columns=('ID', 'description', 'costs', 'total'), height=15, show='headings')

        self.tree.column('ID', width=150, anchor=tk.CENTER)
        self.tree.column('description', width=200, anchor=tk.CENTER)
        self.tree.column('costs', width=150, anchor=tk.CENTER)
        self.tree.column('total', width=150, anchor=tk.CENTER)

        self.tree.heading('ID', text='Мои работы')
        self.tree.heading('description', text='Все задания')
        self.tree.heading('costs', text='Рейтинг')
        self.tree.heading('total', text='Вход')

        self.tree.pack()

    def open_dialog(self):
        Child()


class Child(tk.Toplevel):
    def __init__(self):
        super().__init__(root)
        self.init_child()

    def init_child(self):
        self.title('Вы ещё не зарегистрированы ?')
        self.geometry('400x200+400+300')
        self.resizable(False, False)

        label_description = tk.Label(self, text='Имя')
        label_description.place(x=50, y=50)
        label_mail = tk.Label(self, text='Пол')
        label_mail.place(x=50, y=80)
        label_select = tk.Label(self, text='Почта')
        label_select.place(x=50, y=110)
        label_sum = tk.Label(self, text='Пароль')
        label_sum.place(x=50, y=140)

        self.entry_description = ttk.Entry(self)
        self.entry_description.place(x=200, y=50)

        self.entry_money = ttk.Entry(self)
        self.entry_money.place(x=200, y=110)

        self.combobox = ttk.Combobox(self, values=[u'M', u'Go'])
        self.combobox.current(0)
        self.combobox.place(x=200, y=80)

        self.entry_parol = ttk.Entry(self)
        self.entry_parol.place(x=200, y=140)

        btn_cancel = ttk.Button(self, text='Закрыть', command=self.destroy)
        btn_cancel.place(x=300, y=170)

        btn_ok = ttk.Button(self, text='Добавить')
        btn_ok.place(x=220, y=170)
        btn_ok.bind('<Button-1>')

        self.grab_set()
        self.focus_set()



if __name__ == "__main__":
    root = tk.Tk()
    app = Main(root)
    app.pack()
    root.title("oop")
    root.geometry("650x340+300+200")
    root.resizable(False, False)
    root.mainloop()