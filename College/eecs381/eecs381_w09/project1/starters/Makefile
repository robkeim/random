CC = gcc
LD = gcc

CFLAGS = -c -pedantic -std=c89 -Wmissing-prototypes -Wall
LFLAGS = -pedantic -Wall

OBJS = p1.o Ordered_array.o Action_item.o p1_globals.o Utilities.o
PROG = p1exe

default: $(PROG)

$(PROG): $(OBJS)
	$(LD) $(LFLAGS) $(OBJS) -o $(PROG)

p1.o: p1.c Ordered_array.h Action_item.h p1_globals.h Utilities.h
	$(CC) $(CFLAGS) p1.c

Ordered_array.o: Ordered_array.c Ordered_array.h Utilities.h
	$(CC) $(CFLAGS) Ordered_array.c

Action_item.o: Action_item.c Action_item.h p1_globals.h Utilities.h
	$(CC) $(CFLAGS) Action_item.c

p1_globals.o: p1_globals.c p1_globals.h
	$(CC) $(CFLAGS) p1_globals.c

Utilities.o: Utilities.c Utilities.h p1_globals.h
	$(CC) $(CFLAGS) Utilities.c

clean:
	rm -f *.o

real_clean:
	rm -f *.o
	rm -f p1exe

