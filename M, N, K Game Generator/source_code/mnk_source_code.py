# Tugas 3
# Nama		: Muzakki Hassan Azmi
# NPM		: 1606917632
# Kelas		: DDP 1 - Kelas B
# Asdos		: Wira Abdillah Siregar
# Python 3.5.2

import datetime
from tkinter import *
from tkinter import messagebox

class Menu:	# membuat main menu
	def __init__(self, window):
		self.save = open("mnksave.txt")
		self.window = window
		self.mainFrame = Frame(self.window)
		self.mainFrame.pack()

		self.frameC = Frame(self.mainFrame)
		self.frameC.pack(pady = 20)
		self.frameN = Frame(self.mainFrame)
		self.frameN.pack()
		self.frameM = Frame(self.mainFrame)
		self.frameM.pack(pady = 8)

		self.board = Canvas(self.frameC, height = 270, width = 520)
		self.board.grid(row = 0, column = 0)
		self.img = PhotoImage(file = "mnk.gif")
		self.back = self.board.create_image(260, 135, image = self.img)

		self.labName1 = Label(self.frameN, text = "Player 1 :")
		self.name1 = StringVar()
		self.entName1 = Entry(self.frameN, textvariable = self.name1, width = 22)
		self.labName1.grid(row = 0, column = 0, padx = 15, pady = 6)
		self.entName1.grid(row = 0, column = 1)

		self.labName2 = Label(self.frameN, text = "Player 2 :")
		self.name2 = StringVar()
		self.entName2 = Entry(self.frameN, textvariable = self.name2, width = 22)
		self.labName2.grid(row = 1, column = 0, padx = 15, pady = 6)
		self.entName2.grid(row = 1, column = 1)

		self.jalan = Button(self.frameN, text = "Play", command = self.run, width = 6, pady = 10, padx = 2)
		self.jalan.grid(row = 0, column = 2, rowspan = 2, padx = 20)
		self.sejarah = Button(self.frameN, text = "Game History", command = self.history, width = 6, pady = 10, padx = 28)
		self.sejarah.grid(row = 0, column = 3, rowspan = 2)

		self.labKol = Label(self.frameM, text = "Column (M) :")
		self.numKol = StringVar()
		self.entKol = Entry(self.frameM, textvariable = self.numKol, width = 3)
		self.labKol.grid(row = 0, column = 0, padx = 15, pady = 6)
		self.entKol.grid(row = 0, column = 1)

		self.labBar = Label(self.frameM, text = "Row (N) :")
		self.numBar = StringVar()
		self.entBar = Entry(self.frameM, textvariable = self.numBar, width = 3)
		self.labBar.grid(row = 0, column = 2, padx = 15, pady = 6)
		self.entBar.grid(row = 0, column = 3)
		
		self.labK = Label(self.frameM, text = "Win Condition (K) :")
		self.numK = StringVar()
		self.entK = Entry(self.frameM, textvariable = self.numK, width = 3)
		self.labK.grid(row = 0, column = 4, padx = 15, pady = 6)
		self.entK.grid(row = 0, column = 5)

	def run(self): # fungsi yang terjadi jika memencet button "Play"
		global theWindow

		# membatasi dari input user yang "aneh-aneh", nama yang terlalu panjang atau value M, N, K yang terlalu besar
		if len(self.name1.get()) > 12 or len(self.name2.get()) > 12:
			messagebox.showinfo("Warning", "Player names are maximum 12 characters!")
		elif self.numKol.get() == "" or self.numBar.get() == "" or self.numK.get() == "":
			messagebox.showinfo("Warning", "Please enter a number for M, N, and K!")
		elif self.numKol.get().isdigit() == False or self.numBar.get().isdigit() == False or self.numK.get().isdigit() == False:
			messagebox.showinfo("Warning", "M, N, K input are numbers only!")
		elif int(self.numKol.get()) > 15 or int(self.numBar.get()) > 15 or int(self.numK.get()) > 15:
			messagebox.showinfo("Warning", "M, N, K input are maxed at 15!")
		else:

			# menghapus main menu kemudian memulai permainan
			self.mainFrame.destroy()
			self.mainFrame = Frame(self.window)
			self.mainFrame.pack()

			self.frameI = Frame(self.mainFrame)
			self.frameI.pack()
			self.frameC = Frame(self.mainFrame)
			self.frameC.pack()
			self.frameM = Frame(self.mainFrame)
			self.frameM.pack()

			self.labInfo = Label(self.frameI, text = menu.name1.get() + "'s turn\nTurn : 1")
			self.labInfo.grid(pady = 15)
			self.board = Canvas(self.frameC, bg = "#bababa", height = int(self.numBar.get()) * 50 + 5, width = int(self.numKol.get()) * 50 + 5)
			self.board.grid(row = 1, column = 0)

			# inovasi saya menambah fitur undo dalam game ini
			self.undoList = []
			self.undo = Button(menu.frameM, text = "Undo", width = 6, pady = 10, padx = 10)
			self.undo.grid(row = 1, column = 0, padx = 10)
			self.undoList.append(self.undo)

			self.kembali = Button(self.frameM, text = "Restart", command = self.restart, width = 6, pady = 10, padx = 10)
			self.kembali.grid(row = 1, column = 1, padx = 10)
			self.info = Label(self.frameM, text = "M : " + menu.numKol.get() + "   N : " + menu.numBar.get() + "   K : " + menu.numK.get())
			self.info.grid(row = 0, column = 0, columnspan = 2, pady = 12)

			if int(self.numKol.get()) < 6:	
				theWindow.minsize(width = 375, height = int(self.numBar.get()) * 50 + 217)
				theWindow.maxsize(width = 375, height = int(self.numBar.get()) * 50 + 217)
			else:
				theWindow.minsize(width = int(self.numKol.get()) * 50 + 75, height = int(self.numBar.get()) * 50 + 217)
				theWindow.maxsize(width = int(self.numKol.get()) * 50 + 75, height = int(self.numBar.get()) * 50 + 217)
			
			# beberapa variabel penting
			self.win = False 	# menandakan sudah ada pemenang atau belum
			self.draw = False 	# menandakan permainan seri atau tidak
			self.turn = 0 	# menghitung jumlah turn pemain
			self.count = 0 	# digunakan saat menghitung dalam pengecekan kemenangan
			self.temp = self.save.read() 	# digunakan saat membuat agar history yang baru ditulis di bagian awal notepad
			self.place = {} 	# akan berisi koordinat sebagai key dan kondisinya sebagai value
			self.circList = [] 	# akan berisi semua lingkaran yang telah dibuat

			# generate objek lingkaran untuk dimainkan dalam canvas
			for j in range(int(self.numBar.get())):
				for i in range(int(self.numKol.get())):
					Circle(8 + i * 50, 8 + j * 50, 50 + i * 50, 50 + j * 50, (i, j))

	def history(self):	# fungsi untuk button "Game History", menghapus main menu kemudian membuka game history dari sebuah file notepad
		self.save = open("mnksave.txt")
		self.mainFrame.destroy()
		self.mainFrame = Frame(self.window)
		self.mainFrame.pack()

		self.frameH = Frame(self.mainFrame, pady = 10)
		self.frameH.pack()
		self.frameM = Frame(self.mainFrame, pady = 4)
		self.frameM.pack()

		self.textHis = Text(self.frameH, height = 17, width = 52)
		self.textHis.grid(pady = 10)
		self.textHis.insert(INSERT, self.save.read())
		self.textHis.config(state = DISABLED)
		self.scrollHis = Scrollbar(self.frameH, command = self.textHis.yview)
		self.scrollHis.grid(row = 0, column = 1, sticky = NS)
		self.textHis.config(yscrollcommand = self.scrollHis.set)

		self.clearHis = Button(self.frameM, text = "Clear History", command = self.clear, width = 6, pady = 8, padx = 28)
		self.clearHis.grid(row = 0, column = 0, padx = 10)
		self.backHis = Button(self.frameM, text = "Back", command = self.restart, width = 6, pady = 8, padx = 2)
		self.backHis.grid(row = 0, column = 1, padx = 10)

	def clear(self):	# fungsi untuk button "Clear History" dalam menu game history, menghapus history dalam file text
		self.askClear = messagebox.askquestion("Confirm Clear History", "Are You Sure?", icon='warning')
		if self.askClear == "yes":
			self.save = open("mnksave.txt", "w")
			self.history()

	def restart(self):	# fungsi menghapus menu yang sedang berjalan kemudian kembali ke main menu
		global menu
		global theWindow

		self.mainFrame.destroy()
		menu = Menu(self.window)
		theWindow.minsize(width = 580, height = 462)
		theWindow.maxsize(width = 580, height = 462)

	def removeWin(self):	# fungsi untuk meng-unbind seluruh lingkaran dan menghapus semua undo jika sebuah permainan telah selesai 
		for i in self.circList:
			self.board.tag_unbind(i, "<ButtonPress-1>")
			self.board.itemconfigure(i, activeoutline =	"#bababa", activewidth = 4)
		for i in self.undoList:
			i.destroy()
		self.info.grid(column = 1, columnspan = 1)
		
class Circle:	# setiap lingkaran dimasukkan ke dalam class agar setiap lingkaran memiliki attribut masing-masing (posisi, bind, undo, dll.) 
	def __init__(self, x1, y1, x2, y2, coord):
		global menu

		self.pos = coord 	# setiap lingkaran memiliki koordinat tersendiri
		menu.place[self.pos] = 0 	# setiap koordinat/lingkaran memiliki value masing-masing (0 = belum dipencet, 1 = player 1, 2 = player 2)
		self.circle = menu.board.create_oval(x1, y1, x2, y2, fill = "#ebebeb", outline = "#ebebeb", activeoutline = "#3700ed", activewidth = 4)
		menu.circList.append(self.circle)

		menu.board.tag_bind(self.circle, "<ButtonPress-1>", self.change) 	# setiap lingkaran punya bind masing-masing
		self.changed = False

	def checkWin(self, colorCode):	# fungsi yang berjalan setiap turn serta mengecek kemenangan pada setiap turn
		global menu
		menu.turn += 1
		self.changed = True

		menu.count = 1
		for k in range(int(menu.numK.get())):
			try:
				if menu.place[(self.pos[0] + 1 + k, self.pos[1])] == colorCode: menu.count += 1
				else: break
			except KeyError: break
		for k in range(int(menu.numK.get())):
			try:
				if menu.place[(self.pos[0] - 1 - k, self.pos[1])] == colorCode: menu.count += 1
				else: break
			except KeyError: break
		if menu.count == int(menu.numK.get()): menu.win = True

		menu.count = 1
		for k in range(int(menu.numK.get())):
			try:
				if menu.place[(self.pos[0], self.pos[1] + 1 + k)] == colorCode: menu.count += 1
				else: break
			except KeyError: break
		for k in range(int(menu.numK.get())):
			try:
				if menu.place[(self.pos[0], self.pos[1] - 1 - k)] == colorCode: menu.count += 1
				else: break
			except KeyError: break
		if menu.count == int(menu.numK.get()): menu.win = True

		menu.count = 1
		for k in range(int(menu.numK.get())):
			try:
				if menu.place[(self.pos[0] + 1 + k, self.pos[1] + 1 + k)] == colorCode: menu.count += 1
				else: break
			except KeyError: break
		for k in range(int(menu.numK.get())):
			try:
				if menu.place[(self.pos[0] - 1 - k, self.pos[1] - 1 - k)] == colorCode: menu.count += 1
				else: break
			except KeyError: break
		if menu.count == int(menu.numK.get()): menu.win = True

		menu.count = 1
		for k in range(int(menu.numK.get())):
			try:
				if menu.place[(self.pos[0] - 1 - k, self.pos[1] + 1 + k)] == colorCode: menu.count += 1
				else: break
			except KeyError: break
		for k in range(int(menu.numK.get())):
			try:
				if menu.place[(self.pos[0] + 1 + k, self.pos[1] - 1 - k)] == colorCode: menu.count += 1
				else: break
			except KeyError: break
		if menu.count == int(menu.numK.get()): menu.win = True

		# meletakkan dan menimpa "Undo" button dengan button yang terhubung dengan sebuah lingkaran tanpa menghapus button-button undo yang sudah ada 
		self.undo = Button(menu.frameM, text = "Undo", width = 6, pady = 10, padx = 10, command = self.undoTurn)
		self.undo.grid(row = 1, column = 0, padx = 10)
		menu.undoList.append(self.undo)

		# memberi peringatan jika permainan sudah ada pemenang atau seri serta menambahkan informasi ke notepad game history
		if menu.turn == int(menu.numKol.get()) * int(menu.numBar.get()):
			menu.draw = True
		if menu.win == True or menu.draw == True:
			menu.save = open("mnksave.txt", "r+")
			menu.temp = menu.save.read()
			menu.save.seek(0)
			print(datetime.datetime.now().strftime("%d/%m/%Y %H:%M:%S"), file = menu.save)
			print("M : " + str(menu.numKol.get()) + ", N : " + str(menu.numBar.get()) + ", K : " + str(menu.numK.get()), file = menu.save)
			if menu.win == True:
				if colorCode == 1:
					messagebox.showinfo("Congratulations", menu.name1.get() + " wins! Number of turns : " + str(menu.turn))
					menu.labInfo.config(text = menu.name1.get() + " wins!\nNumber of turns : " + str(menu.turn))
					print("Win - " + menu.name1.get() + " (Player 1)", file = menu.save)
					print("Lose - " + menu.name2.get() + " (Player 2)", file = menu.save)
				else:
					messagebox.showinfo("Congratulations", menu.name2.get() + " wins! Number of turns : " + str(menu.turn))
					menu.labInfo.config(text = menu.name2.get() + " wins!\nNumber of turns : " + str(menu.turn))
					print("Win - " + menu.name2.get() + " (Player 2)", file = menu.save)
					print("Lose - " + menu.name1.get() + " (Player 1)", file = menu.save)
			else:
				messagebox.showinfo("Sorry", "No winner, draw!. Number of turns : " + str(menu.turn))
				menu.labInfo.config(text = "No winner, draw!\nNumber of turns : " + str(menu.turn))
				print("Draw!\n" + menu.name1.get() + " (Player 1) vs " + menu.name2.get() + " (Player 2)", file = menu.save)
			print("Number of turns : " + str(menu.turn), file = menu.save)
			print("--------------------------------------------------", file = menu.save)
			menu.save.write(menu.temp)
			menu.removeWin()

	def change(self, event):	# untuk mengubah warna lingkaran yang dipencet, activeoutline semua lingkaran, dll. sekaligus memanggil fungsi checkWin()
		global menu
		if menu.turn % 2 == 0 and self.changed == False:
			menu.board.itemconfigure(self.circle, fill = "#3700ed", outline = "#3700ed")
			menu.place[self.pos] = 1
			menu.labInfo.config(text = menu.name2.get() + "'s turn\nTurn : " + str(menu.turn + 2))
			for i in menu.circList:
				menu.board.itemconfigure(i, activeoutline = "#ed0026", activewidth = 4)
			self.checkWin(1)
		elif menu.turn % 2 == 1 and self.changed == False:
			menu.board.itemconfigure(self.circle, fill = "#ed0026", outline = "#ed0026")
			menu.place[self.pos] = 2
			menu.labInfo.config(text = menu.name1.get() + "'s turn\nTurn : " + str(menu.turn + 2))
			for i in menu.circList:
				menu.board.itemconfigure(i, activeoutline = "#3700ed", activewidth = 4)
			self.checkWin(2)

	def undoTurn(self):	# fungsi jika button "Undo" dipencet
		global menu
		if menu.turn % 2 == 0:
			for i in menu.circList:
				menu.board.itemconfigure(i, activeoutline = "#ed0026", activewidth = 4)
			menu.labInfo.config(text = menu.name2.get() + "'s turn\nTurn : " + str(menu.turn))
		else:
			for i in menu.circList:
				menu.board.itemconfigure(i, activeoutline = "#3700ed", activewidth = 4)
			menu.labInfo.config(text = menu.name1.get() + "'s turn\nTurn : " + str(menu.turn))

		self.changed = False
		menu.board.itemconfigure(self.circle, fill = "#ebebeb", outline = "#ebebeb")
		menu.place[self.pos] = 0
		self.undo.destroy()
		menu.turn -= 1

# mengkonfigurasi window utama dan satu-satunya dalam sekali pemanggilan program
theWindow = Tk()
theWindow.title("M, N, K Game Generator")
theWindow.iconbitmap("mnk.ico")
theWindow.resizable(width = False, height = False)
theWindow.minsize(width = 580, height = 462)
theWindow.maxsize(width = 580, height = 462)
menu = Menu(theWindow)
theWindow.mainloop()