CC = g++
LD = g++

EXTRAFLAGS = -Wuninitialized -O3

CFLAGS = -c -g -pedantic -ansi -Wall -fno-elide-constructors
LFLAGS = -g -pedantic -Wall

OBJS = p2_main.o Record.o Collection.o p2_globals.o String.o Utility.o
PROG = p2exe

D_S1 = d_s1exe
D_S2 = d_s2exe
D_S3 = d_s3exe
D_S4 = d_s4exe
D_OL1 = d_ol1exe
D_OL2 = d_ol2exe
D_OL3 = d_ol3exe
TESTS = $(D_S1) $(D_S2) $(D_S3) $(D_S4) $(D_OL1) $(D_OL2) $(D_OL3)

default: $(PROG) $(TESTS)

$(PROG): $(OBJS)
	$(LD) $(LFLAGS) $(OBJS) -o $(PROG)

$(D_S1): d_s1.o String.o Utility.o
	$(LD) $(LFLAGS) d_s1.o String.o Utility.o -o $(D_S1)

$(D_S2): d_s2.o String.o Utility.o
	$(LD) $(LFLAGS) d_s2.o String.o Utility.o -o $(D_S2)

$(D_S3): d_s3.o String.o Utility.o
	$(LD) $(LFLAGS) d_s3.o String.o Utility.o -o $(D_S3)

$(D_S4): d_s4.o String.o Utility.o
	$(LD) $(LFLAGS) d_s4.o String.o Utility.o -o $(D_S4)

$(D_OL1): d_ol1.o String.o Utility.o p2_globals.o
	$(LD) $(LFLAGS) d_ol1.o String.o Utility.o p2_globals.o -o $(D_OL1)

$(D_OL2): d_ol2.o String.o Utility.o p2_globals.o
	$(LD) $(LFLAGS) d_ol2.o String.o Utility.o p2_globals.o -o $(D_OL2)

$(D_OL3): d_ol3.o String.o Utility.o p2_globals.o
	$(LD) $(LFLAGS) d_ol3.o String.o Utility.o p2_globals.o -o $(D_OL3)

d_s1.o: d_s1.cpp String.h Utility.h
	$(CC) $(CFLAGS) d_s1.cpp

d_s2.o: d_s2.cpp String.h Utility.h
	$(CC) $(CFLAGS) d_s2.cpp

d_s3.o: d_s3.cpp String.h Utility.h
	$(CC) $(CFLAGS) d_s3.cpp
	
d_s4.o: d_s4.cpp String.h Utility.h
	$(CC) $(CFLAGS) d_s4.cpp

d_ol1.o: d_ol1.cpp String.h Utility.h Ordered_list.h
	$(CC) $(CFLAGS) d_ol1.cpp
		
d_ol2.o: d_ol2.cpp String.h Utility.h Ordered_list.h
	$(CC) $(CFLAGS) d_ol2.cpp

d_ol3.o: d_ol3.cpp String.h Utility.h Ordered_list.h
	$(CC) $(CFLAGS) d_ol3.cpp

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
	rm -f *.o *exe *~ .*.swp *.gch savefile1.txt
	
test:
	make
	./test.sh

upload:
	make clean
	scp * robkeim@loginlinux.engin.umich.edu:/afs/umich.edu/user/r/o/robkeim/p2
