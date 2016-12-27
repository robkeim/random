CC = g++
LD = g++

EXTRAFLAGS = -Wuninitialized -O3

CFLAGS = -c -g -pedantic -ansi -Wall -fno-elide-constructors
LFLAGS = -g -pedantic -Wall

OBJS := $(patsubst %.cpp, %.o, $(wildcard *.cpp))
PROG = p4exe

default: $(PROG)

$(PROG): $(OBJS)
	$(LD) $(LFLAGS) $(OBJS) -o $(PROG)

#%.o: %.h

Controller.o: Island.h Model.h Ship.h Ship_factory.h Utility.h View.h
Cruiser.o:
Geometry.o:
Island.o: Geometry.h Sim_object.h
Model.o: Island.h Utility.h Ship.h Ship_factory.h View.h
Navigation.o: Geometry.h
p4_main.o:
Ship.o: Island.h Sim_object.h Track_base.h Utility.h
Ship_factory.o: Geometry.h Utility.h
Tanker.o: Island.h Utility.h
Track_base.o: Geometry.h Navigation.h
Utility.o:
View.o: Geometry.h Utility.h
Warship.o: Geometry.h Ship.h Utility.h

%.o: %.cpp
	$(CC) $(CFLAGS) $<

clean:
	rm -f *.o *exe *~ .*.swp *.gch
	
test:
	make
	./test.sh

upload:
	make clean
	scp * robkeim@loginlinux.engin.umich.edu:/afs/umich.edu/user/r/o/robkeim/p4

