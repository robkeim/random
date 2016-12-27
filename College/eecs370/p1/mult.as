		lw		0		2		mcand		register 2 - mcand
		lw		0		3		mplier		register 3 - mplier
		lw		0		4		one			register 4 - counter
		lw		0		6		negOne		register 6 - -1
		lw		0		7		maxVal		resister 7 - max
start	nand	2		4		5			mcand ~& counter
		nand		5		6		5			(mcand ~& counter) ~& -1
		beq		5		0		skip		skips addition if nth bit is zero
		add		1		3		1			adds mplier to result
skip	add			4		4		4			counter << 1
		add		3		3		3			mplier << 1
		beq		4		7		end		
		beq		0		0		start		loops back to start
end		halt
one		.fill		1
negOne	.fill	-1
maxVal	.fill	65536
zero	.fill	0		
mcand  	.fill	32766
mplier 	.fill 	10383
