# Makefile for project 1
# The comments below introduce how a makefile works.
#
# This makefile assumes that all of the source files and compiler output are
# in the same directory as this makefile.
# 
# If this file has the default name of "Makefile", you 
# can use the Unix/Linux make command as follows:
#
# make p1Lexe - Build an executable named "p1Lexe" that uses 
# Ordered_container_list as the implementation of Ordered_container.
#
# make p1Aexe - Build an executable named "p1Aexe" that uses 
# Ordered_container_array as the implementation of Ordered_container.
#
# make - Build both executables.
#
# make clean - Delete the .o files.
#
# make real_clean - Delete the .o files and the two executables.

# Note how variables are used for ease of modification.

CC = gcc
LD = gcc

# specify compile and link options
# For debugging -Wuninitialized -O3 (must be used together)
CFLAGS = -g -c -ansi -std=c89 -pedantic -Wmissing-prototypes -Wall
LFLAGS = -g -pedantic -Wall

OBJS = p1_main.o Record.o Collection.o p1_globals.o Utility.o

OBJS_L = Ordered_container_list.o
OBJS_A = Ordered_container_array.o

OBJS_TEST = Utility.o p1_globals.o

EX_L = p1Lexe
EX_A = p1Aexe

EXECS = $(EX_L) $(EX_A)

TEST_1L = test1L
TEST_1A = test1A
TEST_2L = test2L
TEST_2A = test2A
TEST_3L = test3L
TEST_3A = test3A
TEST_1R = test1R
TEST_1CL = test1CL
TEST_1CA = test1CA

TESTS = $(TEST_1L) $(TEST_1A) $(TEST_2L) $(TEST_2A) $(TEST_3L) $(TEST_3A) $(TEST_1R) $(TEST_1CL) $(TEST_1CA)

# following asks for both executables to be built
default:  $(EXECS) $(TESTS)

# to build this executable, check to see if any of the listed object files
# need to be rebuilt and do so, then link to create the executable
$(EX_L): $(OBJS) $(OBJS_L)
	$(LD) $(LFLAGS) $(OBJS) $(OBJS_L) -o $(EX_L)

$(EX_A): $(OBJS) $(OBJS_A)
	$(LD) $(LFLAGS) $(OBJS) $(OBJS_A) -o $(EX_A)

$(TEST_1L): Ordered_container_demo1.o $(OBJS_TEST) $(OBJS_L)
	$(LD) $(LFLAGS) $(OBJS_TEST) $(OBJS_L) Ordered_container_demo1.o -o $(TEST_1L)

$(TEST_1A): Ordered_container_demo1.o $(OBJS_TEST) $(OBJS_A)
	$(LD) $(LFLAGS) $(OBJS_TEST) $(OBJS_A) Ordered_container_demo1.o -o $(TEST_1A)

$(TEST_2L): Ordered_container_demo2.o $(OBJS_TEST) $(OBJS_L)
	$(LD) $(LFLAGS) $(OBJS_TEST) $(OBJS_L) Ordered_container_demo2.o -o $(TEST_2L)

$(TEST_2A): Ordered_container_demo2.o $(OBJS_TEST) $(OBJS_A)
	$(LD) $(LFLAGS) $(OBJS_TEST) $(OBJS_A) Ordered_container_demo2.o -o $(TEST_2A)

$(TEST_3L): Ordered_container_demo3.o $(OBJS_TEST) $(OBJS_L)
	$(LD) $(LFLAGS) $(OBJS_TEST) $(OBJS_L) Ordered_container_demo3.o -o $(TEST_3L)

$(TEST_3A): Ordered_container_demo3.o $(OBJS_TEST) $(OBJS_A)
	$(LD) $(LFLAGS) $(OBJS_TEST) $(OBJS_A) Ordered_container_demo3.o -o $(TEST_3A)

$(TEST_1R): Record.o $(OBJS_TEST) Record_demo1.c
	$(LD) $(FLAGS) Record.o $(OBJS_TEST) Record_demo1.c -o $(TEST_1R)

$(TEST_1CL): Record.o Collection.o $(OBJS_TEST) Ordered_container_list.o Collection_demo1.c
	$(LD) $(FLAGS) Record.o Collection.o $(OBJS_TEST) Ordered_container_list.o Collection_demo1.c -o $(TEST_1CL)

$(TEST_1CA): Record.o Collection.o $(OBJS_TEST) Ordered_container_array.o Collection_demo1.c
	$(LD) $(FLAGS) Record.o Collection.o $(OBJS_TEST) Ordered_container_array.o Collection_demo1.c -o $(TEST_1CA)

# to build this object module, check the timestamps of what it depends on, and
# if any are more recent than an existing p1_main.o, then recompile p1_main.c
p1_main.o: p1_main.c Ordered_container.h Record.h Collection.h p1_globals.h Utility.h
	$(CC) $(CFLAGS) p1_main.c

Ordered_container_list.o: Ordered_container_list.c Ordered_container.h p1_globals.h Utility.h
	$(CC) $(CFLAGS) Ordered_container_list.c

Ordered_container_array.o: Ordered_container_array.c Ordered_container.h p1_globals.h Utility.h
	$(CC) $(CFLAGS) Ordered_container_array.c

Record.o: Record.c Record.h p1_globals.h Utility.h
	$(CC) $(CFLAGS) Record.c

Collection.o: Collection.c Collection.h Ordered_container.h Record.h p1_globals.h Utility.h
	$(CC) $(CFLAGS) Collection.c

p1_globals.o: p1_globals.c p1_globals.h
	$(CC) $(CFLAGS) p1_globals.c

Ordered_container_demo1.o: Ordered_container_demo1.c Utility.h p1_globals.c
	$(CC) $(CFLAGS) Ordered_container_demo1.c Utility.h p1_globals.c

Ordered_container_demo2.o: Ordered_container_demo2.c Utility.h p1_globals.c
	$(CC) $(CFLAGS) Ordered_container_demo2.c Utility.h p1_globals.c

Ordered_container_demo3.o: Ordered_container_demo3.c Utility.h p1_globals.c
	$(CC) $(CFLAGS) Ordered_container_demo3.c Utility.h p1_globals.c

#include Record.h here only if your Utilities.c must include it.
Utility.o: Utility.c Utility.h p1_globals.h Record.h
	$(CC) $(CFLAGS) Utility.c

# other shell commands can appear as "things to do" - not just compilations, etc.
clean:
	rm -f *.o

real_clean:
	rm -f *.o $(EXECS) $(TESTS) Utility.h.gch *~ savefile1.txt tmp

test:
	make
	./test.sh

submit:
	make
	./test.sh
	make real_clean
	make -f submit_Makefile
	./p1Lexe < normal_in.txt > tmp
	diff normalL_out.txt tmp
	./p1Aexe < normal_in.txt > tmp
	diff normalA_out.txt tmp
	make real_clean
	rm *demo*.c

upload:
	make real_clean
	scp * robkeim@loginlinux.engin.umich.edu:/afs/umich.edu/user/r/o/robkeim/p1
