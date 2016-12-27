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
CFLAGS = -c -ansi -std=c89 -pedantic -Wmissing-prototypes -Wall
LFLAGS = -pedantic -Wall

OBJS = p1_main.o Record.o Collection.o p1_globals.o Utility.o
OBJS_L = Ordered_container_list.o
OBJS_A = Ordered_container_array.o
EX_L = p1Lexe
EX_A = p1Aexe

# following asks for both executables to be built
default:  $(EX_L) $(EX_A)

# to build this executable, check to see if any of the listed object files
# need to be rebuilt and do so, then link to create the executable
$(EX_L): $(OBJS) $(OBJS_L)
	$(LD) $(LFLAGS) $(OBJS) $(OBJS_L) -o $(EX_L)

$(EX_A): $(OBJS) $(OBJS_A)
	$(LD) $(LFLAGS) $(OBJS) $(OBJS_A) -o $(EX_A)

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

#include Record.h here only if your Utilities.c must include it.
Utility.o: Utility.c Utility.h p1_globals.h Record.h
	$(CC) $(CFLAGS) Utility.c

# other shell commands can appear as "things to do" - not just compilations, etc.
clean:
	rm -f *.o

real_clean:
	rm -f *.o
	rm -f $(EX_L)
	rm -f $(EX_A)

