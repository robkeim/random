CC = g++
LD = g++

CFLAGS = -c -pedantic -ansi -Wall -fno-elide-constructors
LFLAGS = -pedantic -Wall

OBJS = p2_main.o Record.o Collection.o p2_globals.o String.o Utility.o
PROG = p2exe
SDEMO3 = sdemo3exe

default: $(PROG)
sdemo3: $(SDEMO3)

$(SDEMO3): String_demo3.o String.o Utility.o
	$(LD) $(LFLAGS) String_demo3.o String.o Utility.o -o $(SDEMO3)

String_demo3.o: String_demo3.cpp String.h Utility.h
	$(CC) $(CFLAGS) String_demo3.cpp

$(PROG): $(OBJS)
	$(LD) $(LFLAGS) $(OBJS) -o $(PROG)

p2_main.o: p2_main.cpp Ordered_list.h p2_globals.h Record.h String.h Utility.h
	$(CC) $(CFLAGS) p2_main.cpp

Record.o: Record.cpp Record.h String.h Utility.h
	$(CC) $(CFLAGS) Record.cpp

Collection.o: Collection.cpp Collection.h Ordered_list.h p2_globals.h String.h Utility.h
	$(CC) $(CFLAGS) Collection.cpp

p2_globals.o: p2_globals.cpp p2_globals.h
	$(CC) $(CFLAGS) p2_globals.cpp

String.o: String.cpp String.h Utility.h
	$(CC) $(CFLAGS) String.cpp

Utility.o: Utility.cpp Utility.h String.h
	$(CC) $(CFLAGS) Utility.cpp

clean:
	rm -f *.o

real_clean:
	rm -f *.o
	rm -f *exe

