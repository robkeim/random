CC = g++
LD = g++

EXTRAFLAGS = -Wuninitialized -O3

CFLAGS = -c -g -pedantic -ansi -Wall -fno-elide-constructors
LFLAGS = -g -pedantic -Wall

OBJS = p3_main.o Record.o Collection.o Utility.o
PROG = p3exe

default: $(PROG)

$(PROG): $(OBJS)
	$(LD) $(LFLAGS) $(OBJS) -o $(PROG)

p3_main.o: p3_main.cpp Record.h Utility.h
	$(CC) $(CFLAGS) p3_main.cpp

Record.o: Record.cpp Record.h Utility.h
	$(CC) $(CFLAGS) Record.cpp

Collection.o: Collection.cpp Collection.h Utility.h
	$(CC) $(CFLAGS) Collection.cpp

Utility.o: Utility.cpp Utility.h
	$(CC) $(CFLAGS) Utility.cpp
	
clean:
	rm -f *.o *exe *~ .*.swp *.gch savefile1.txt
	
test:
	make
	./test.sh

upload:
	make clean
	scp * robkeim@loginlinux.engin.umich.edu:/afs/umich.edu/user/r/o/robkeim/p3

