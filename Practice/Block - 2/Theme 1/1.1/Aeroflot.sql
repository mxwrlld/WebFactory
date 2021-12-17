use Aeroflot;

CREATE TABLE Company (
	[ID_comp] int,
	[name] char(10),

	CONSTRAINT PK_id PRIMARY KEY(ID_comp)
);

CREATE TABLE Trip (
	[trip_no] int,
	[ID_comp] int NOT NULL, 
	[plane] nchar(10) NOT NULL,
	[town_from] nchar(25) NOT NULL,
	[town_to] nchar(25) NOT NULL,
	[time_out] datetime2 NOT NULL,
	[time_in] datetime2 NOT NULL,
	CONSTRAINT PK_trip_no PRIMARY KEY(trip_no),
	CONSTRAINT FK_ID_comp FOREIGN KEY(ID_comp)
		REFERENCES Company(ID_comp)
);

CREATE TABLE Passenger (
	[ID_psg] int,
	[name] nchar(20) NOT NULL,
	CONSTRAINT PK_ID_psg PRIMARY KEY(ID_psg)
);

CREATE TABLE Pass_in_trip (
	[trip_no] int NOT NULL,
	[date] datetime2 NOT NULL,
	[ID_psg] int NOT NULL,
	[place] nchar(10) NOT NULL,
	CONSTRAINT PK_pit PRIMARY KEY([trip_no], [date], [ID_psg]),
	CONSTRAINT FK_trip FOREIGN KEY([trip_no])
		REFERENCES Trip([trip_no]),
	CONSTRAINT FK_psg FOREIGN KEY([ID_psg])
		REFERENCES Passenger([ID_psg]),

	CONSTRAINT check_place CHECK
		(place LIKE '[1-28][a-d]')
);


