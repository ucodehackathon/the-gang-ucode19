import 'package:firebase_database/firebase_database.dart';
import 'package:firebase_database/ui/firebase_animated_list.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_slidable/flutter_slidable.dart';
import 'package:register_adidas/fonts/my_flutter_app_icons.dart';
import 'package:register_adidas/models/player.dart';
import 'package:register_adidas/models/session.dart';
import 'package:register_adidas/utils/firebase.dart';
import 'package:register_adidas/utils/utils.dart';

class ScreenLanding extends StatefulWidget {
  @override
  ScreenLandingState createState() {
    return ScreenLandingState();
  }
}

class ScreenLandingState extends State<ScreenLanding>
    with SingleTickerProviderStateMixin {
  final GlobalKey<FormState> _formKey = new GlobalKey<FormState>();
  final GlobalKey<FormState> _formPlayerKey = new GlobalKey<FormState>();
  SlidableController slidableController;

  @override
  void initState() {
    slidableController = new SlidableController();
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Sessions'),
      ),
      body: FirebaseAnimatedList(
        query: Database().getSessions(),
        itemBuilder: (BuildContext context, DataSnapshot snapshot,
            Animation<double> animation, int index) {
          return SizeTransition(
            sizeFactor: animation,
            child: showSession(context, snapshot),
          );
        },
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () => showSessionDialog(),
        child: Icon(Icons.event),
      ),
    );
  }

  showSession(BuildContext context, DataSnapshot snapshot) {
    var session = Session.fromMap(snapshot.value);
    return Padding(
      padding: const EdgeInsets.all(1.0),
      child: Slidable(
        key: ObjectKey(snapshot),
        controller: slidableController,
        delegate: SlidableBehindDelegate(),
        actionExtentRatio: 0.25,
        child: Container(
          color: Colors.white,
          child: new ListTile(
            title: ExpansionTile(
              children: session.players
                  .map((player) => getPlayerView(player))
                  .toList(),
              title: getSessionView(session),
            ),
          ),
        ),
        actions: <Widget>[
          IconSlideAction(
            caption: 'Player',
            color: Colors.blue,
            icon: Icons.accessibility,
            onTap: () => showPlayerDialog(session),
          ),
        ],
        secondaryActions: <Widget>[
          IconSlideAction(
            caption: 'Delete',
            color: Colors.red,
            icon: Icons.delete,
            onTap: () {
              deleteSession(snapshot);
              Scaffold.of(context).showSnackBar(SnackBar(
                  content: Text("Session deleted"),
                  action: SnackBarAction(
                      label: "UNDO",
                      onPressed: () {
                        //To undo deletion
                        undoDeletion(snapshot);
                      })));
            },
          ),
        ],
      ),
    );
  }

  void deleteSession(DataSnapshot snapshot) {
    Database().removeSession(snapshot.key);
  }

  void undoDeletion(DataSnapshot snapshot) =>
      Database().createSession(Session.fromMap(snapshot.value));

  Widget getSessionView(Session session) {
    return Column(
      children: <Widget>[
        Padding(
          padding: const EdgeInsets.symmetric(vertical: 8.0),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceAround,
            children: <Widget>[
              Flexible(
                flex: 4,
                child: Text(
                  session.name,
                  style: TextStyle(fontWeight: FontWeight.bold, fontSize: 22.0),
                ),
              ),
              Flexible(
                child: Row(
                  children: <Widget>[
                    Padding(
                      padding: const EdgeInsets.only(right: 8.0),
                      child: Text('${session.numberOfPlayers}',
                          style: TextStyle(fontSize: 18.0)),
                    ),
                    Icon(Icons.group)
                  ],
                ),
              ),
            ],
          ),
        ),
        Padding(
          padding: const EdgeInsets.symmetric(vertical: 8.0),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceAround,
            children: <Widget>[
              Row(
                children: <Widget>[
                  Padding(
                    padding: const EdgeInsets.only(right: 8.0),
                    child: Icon(MyFlutterApp.termometro),
                  ),
                  Text(Utils.capitalLetter(session.weather)),
                ],
              ),
              Row(
                children: <Widget>[
                  Padding(
                    padding: const EdgeInsets.only(right: 8.0),
                    child: Icon(Icons.brightness_4),
                  ),
                  Text(Utils.capitalLetter(session.timeOfDay)),
                ],
              ),
            ],
          ),
        ),
        Padding(
          padding: const EdgeInsets.symmetric(vertical: 8.0),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceAround,
            children: <Widget>[
              Row(
                children: <Widget>[
                  Padding(
                    padding: const EdgeInsets.only(right: 8.0),
                    child: Icon(MyFlutterApp.hojas_de_pasto_en_silueta),
                  ),
                  Text(Utils.capitalLetter(session.fieldType)),
                ],
              ),
              Row(
                children: <Widget>[
                  Padding(
                    padding: const EdgeInsets.only(right: 8.0),
                    child: Icon(MyFlutterApp.sneaker),
                  ),
                  Text(Utils.capitalLetter(session.shoeType)),
                ],
              ),
            ],
          ),
        ),
      ],
    );
  }

  Widget getPlayerView(Player player) {
    return Card(
      child: Padding(
        padding: const EdgeInsets.all(8.0),
        child: Row(
          children: <Widget>[
            Expanded(
                child: Stack(
              alignment: Alignment.center,
              children: <Widget>[
                Icon(
                  MyFlutterApp.shirt,
                  size: 50,
                ),
                Text('${player.number}')
              ],
            )),
            Expanded(
              flex: 5,
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: <Widget>[
                  Column(
                    mainAxisAlignment: MainAxisAlignment.spaceAround,
                    children: <Widget>[
                      Row(
                        children: <Widget>[
                          Icon(
                            MyFlutterApp.height,
                            size: 30,
                          ),
                          Text('${player.height} cm'),
                        ],
                      ),
                      Divider(),
                      Row(
                        children: <Widget>[
                          Icon(
                            MyFlutterApp.scale,
                            size: 30,
                          ),
                          Text('${player.weight} Kg'),
                        ],
                      ),
                    ],
                  ),
                  Column(
                    mainAxisAlignment: MainAxisAlignment.spaceAround,
                    children: <Widget>[
                      Row(
                        children: <Widget>[
                          Icon(
                            MyFlutterApp.birthday_cake,
                            size: 30,
                          ),
                          Text('${player.age} years'),
                        ],
                      ),
                      Divider(),
                      Row(
                        children: <Widget>[
                          Icon(
                            MyFlutterApp.foot_print,
                            size: 30,
                          ),
                          Text(
                              '${Utils.capitalLetter(player.mainFoot)} footed'),
                        ],
                      ),
                    ],
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }

  Session session = Session.empty();

  String _field = '';
  List<String> _fields = <String>[
    '',
    'grass',
    'artificial turf',
    'dirt',
  ];

  String _shoe = '';
  List<String> _shoes = <String>[
    '',
    'Running',
    'Trail Running',
    'Minimalist',
    'Walking',
    'Cross',
    'Basketball',
    'Soccer',
    'Lacrosse',
  ];

  String _timeOfDay = '';
  List<String> _timesOfDay = <String>[
    '',
    'morning',
    'afternoon',
    'evening',
  ];

  String _weather = '';
  List<String> _weathers = <String>[
    '',
    'rainy',
    'sunny',
    'windy',
    'cloudy',
    'foggy',
  ];

  showSessionDialog() {
//    Database().createSession(Mock.sessions[0]);
    showDialog(
        context: context,
        builder: (BuildContext context) {
          return AlertDialog(
            title: Text('Create session'),
            content: getDialogSessionView(),
            actions: <Widget>[
              FlatButton(
                  onPressed: () => Navigator.of(context).pop(),
                  child: Text('Cancel')),
              FlatButton(
                onPressed: () => checkSessionForm(),
                child: Text('Confirm'),
              ),
            ],
          );
        });
  }

  getDialogSessionView() {
    return SingleChildScrollView(
      child: Form(
        key: _formKey,
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: <Widget>[
            TextFormField(
              onSaved: (val) => session.name = val,
              decoration: const InputDecoration(
                icon: const Icon(Icons.event),
                hintText: 'Session name',
                labelText: 'Session name',
              ),
            ),
            FormField(
              builder: (FormFieldState state) {
                return InputDecorator(
                  decoration: InputDecoration(
                    icon: const Icon(MyFlutterApp.hojas_de_pasto_en_silueta),
                    labelText: 'Field',
                  ),
                  isEmpty: _field == '',
                  child: DropdownButtonHideUnderline(
                    child: DropdownButton(
                      value: _field,
                      isDense: true,
                      onChanged: (String newValue) {
                        setState(() {
                          session.fieldType = newValue;
                          _field = newValue;
                          state.didChange(newValue);
                        });
                      },
                      items: _fields.map((String value) {
                        return new DropdownMenuItem(
                          value: value,
                          child: new Text(value),
                        );
                      }).toList(),
                    ),
                  ),
                );
              },
            ),
            FormField(
              builder: (FormFieldState state) {
                return InputDecorator(
                  decoration: InputDecoration(
                    icon: const Icon(MyFlutterApp.sneaker),
                    labelText: 'Shoes',
                  ),
                  isEmpty: _shoe == '',
                  child: DropdownButtonHideUnderline(
                    child: DropdownButton(
                      value: _shoe,
                      isDense: true,
                      onChanged: (String newValue) {
                        setState(() {
                          session.shoeType = newValue;
                          _shoe = newValue;
                          state.didChange(newValue);
                        });
                      },
                      items: _shoes.map((String value) {
                        return new DropdownMenuItem(
                          value: value,
                          child: new Text(value),
                        );
                      }).toList(),
                    ),
                  ),
                );
              },
            ),
            FormField(
              builder: (FormFieldState state) {
                return InputDecorator(
                  decoration: InputDecoration(
                    icon: const Icon(MyFlutterApp.termometro),
                    labelText: 'Weather',
                  ),
                  isEmpty: _weather == '',
                  child: DropdownButtonHideUnderline(
                    child: DropdownButton(
                      value: _weather,
                      isDense: true,
                      onChanged: (String newValue) {
                        setState(() {
                          session.weather = newValue;
                          _weather = newValue;
                          state.didChange(newValue);
                        });
                      },
                      items: _weathers.map((String value) {
                        return new DropdownMenuItem(
                          value: value,
                          child: new Text(value),
                        );
                      }).toList(),
                    ),
                  ),
                );
              },
            ),
            FormField(
              builder: (FormFieldState state) {
                return InputDecorator(
                  decoration: InputDecoration(
                    icon: const Icon(Icons.brightness_4),
                    labelText: 'Time of day',
                  ),
                  isEmpty: _timeOfDay == '',
                  child: DropdownButtonHideUnderline(
                    child: DropdownButton(
                      value: _timeOfDay,
                      isDense: true,
                      onChanged: (String newValue) {
                        setState(() {
                          session.timeOfDay = newValue;
                          _timeOfDay = newValue;
                          state.didChange(newValue);
                        });
                      },
                      items: _timesOfDay.map((String value) {
                        return new DropdownMenuItem(
                          value: value,
                          child: new Text(value),
                        );
                      }).toList(),
                    ),
                  ),
                );
              },
            ),
            TextFormField(
              keyboardType: TextInputType.multiline,
              maxLines: 3,
              onSaved: (val) => session.description = val,
              decoration: const InputDecoration(
                icon: const Icon(Icons.chat),
                hintText: 'Description (optional)',
                labelText: 'Description',
              ),
            ),
          ],
        ),
      ),
    );
  }

  checkSessionForm() {
    final FormState form = _formKey.currentState;
    if (form.validate()) {
      form.save();
      session.date = DateTime.now();
      session.sensorType = 'None';
      print(session.toString());
      Database().createSession(session).then((onValue) {
        session = Session.empty();
        _field = '';
        _shoe = '';
        _timeOfDay = '';
        _weather = '';
        Navigator.of(context).pop();
      });
    }
  }

  Player player = Player.empty();

  String _mainFoot = '';
  List<String> _mainFoots = <String>[
    '',
    'left',
    'right',
  ];

  showPlayerDialog(Session session) {
//    Database().createSession(Mock.sessions[0]);
    showDialog(
        context: context,
        builder: (BuildContext context) {
          return AlertDialog(
            title: Text('Create player'),
            content: getDialogPlayerView(),
            actions: <Widget>[
              FlatButton(
                  onPressed: () => Navigator.of(context).pop(),
                  child: Text('Cancel')),
              FlatButton(
                onPressed: () => checkPlayerForm(session),
                child: Text('Confirm'),
              ),
            ],
          );
        });
  }

  getDialogPlayerView() {
    return SingleChildScrollView(
      child: Form(
        key: _formPlayerKey,
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: <Widget>[
            TextFormField(
              onSaved: (val) => player.number = int.parse(val),
              decoration: const InputDecoration(
                icon: const Icon(MyFlutterApp.shirt),
                hintText: 'The number on your t-shirt',
                labelText: 'T-shirt number',
              ),
            ),
            TextFormField(
              onSaved: (val) => player.height = int.parse(val),
              decoration: const InputDecoration(
                icon: const Icon(MyFlutterApp.height),
                hintText: 'Your height (cm)',
                labelText: 'Height (cm)',
              ),
            ),
            TextFormField(
              onSaved: (val) => player.weight = int.parse(val),
              decoration: const InputDecoration(
                icon: const Icon(MyFlutterApp.scale),
                hintText: 'Your weight (Kg)',
                labelText: 'Weight (Kg)',
              ),
            ),
            TextFormField(
              onSaved: (val) => player.age = int.parse(val),
              decoration: const InputDecoration(
                icon: const Icon(MyFlutterApp.birthday_cake),
                hintText: 'Your age',
                labelText: 'Age',
              ),
            ),
            FormField(
              builder: (FormFieldState state) {
                return InputDecorator(
                  decoration: InputDecoration(
                    icon: const Icon(MyFlutterApp.foot_print),
                    labelText: 'Main foot',
                  ),
                  isEmpty: _mainFoot == '',
                  child: DropdownButtonHideUnderline(
                    child: DropdownButton(
                      value: _mainFoot,
                      isDense: true,
                      onChanged: (String newValue) {
                        setState(() {
                          player.mainFoot = newValue;
                          _mainFoot = newValue;
                          state.didChange(newValue);
                        });
                      },
                      items: _mainFoots.map((String value) {
                        return new DropdownMenuItem(
                          value: value,
                          child: new Text(value),
                        );
                      }).toList(),
                    ),
                  ),
                );
              },
            ),
            TextFormField(
              onSaved: (val) => player.shoeSize = int.parse(val),
              decoration: const InputDecoration(
                icon: const Icon(MyFlutterApp.sneaker),
                hintText: 'Your shoes size',
                labelText: 'Shoes size',
              ),
            ),
          ],
        ),
      ),
    );
  }

  checkPlayerForm(Session session) {
    final FormState form = _formPlayerKey.currentState;
    if (form.validate()) {
      form.save();
      print(player);
      session.players.add(player);
      Database()
          .updateSession(
              session.date.millisecondsSinceEpoch.toString(), session)
          .then((onValue) {
        player = Player.empty();
        _mainFoot = '';
        Navigator.of(context).pop();
      });
    }
  }
}
