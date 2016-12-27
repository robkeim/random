CC = g++
LD = g++

CFLAGS = -c -pedantic -ansi -Wall -fno-elide-constructors
LFLAGS = -pedantic -Wall

OBJS = p2.o String.o Action_item.o Utilities.o
PROG = p2exe

default: $(PROG)

$(PROG): $(OBJS)
	$(LD) $(LFLAGS) $(OBJS) -o $(PROG)

p2.o: p2.cpp Ordered_array.h Action_item.h String.h Utilities.h
	$(CC) $(CFLAGS) p2.cpp

String.o: String.cpp String.h Utilities.h
	$(CC) $(CFLAGS) String.cpp

Action_item.o: Action_item.cpp Action_item.h String.h Utilities.h
	$(CC) $(CFLAGS) Action_item.cpp

Utilities.o: Utilities.cpp Utilities.h String.h
	$(CC) $(CFLAGS) Utilities.cpp

clean:
	rm -f *.o

real_clean:
	rm -f *.o
	rm -f *exe

