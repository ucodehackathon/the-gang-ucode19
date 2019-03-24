import 'package:register_adidas/models/player.dart';

class Session {
  String name;
  String fieldType;
  String weather;
  String timeOfDay;
  String shoeType;
  String sensorType;
  String description;
  List<Player> players;
  DateTime date;

  Session.empty() : players = [];

  Session(this.name, this.fieldType, this.weather, this.timeOfDay,
      this.shoeType, this.sensorType, this.description, this.players, this.date);

  Map<String, dynamic> map() => {
        "name": name,
        "fieldType": fieldType,
        "weather": weather,
        "timeOfDay": timeOfDay,
        "shoeType": shoeType,
        "sensorType": sensorType,
        "description": description,
        "players": playersToMap(),
        "date": date.millisecondsSinceEpoch,
      };

  playersToMap() => players.map((player) => player.map()).toList();

  Session.fromMap(map)
      : name = map['name'],
        fieldType = map['fieldType'],
        weather = map['weather'],
        timeOfDay = map['timeOfDay'],
        shoeType = map['shoeType'],
        sensorType = map['sensorType'],
        description = map['description'],
        players = getPlayersFromMap(map['players']),
        date = DateTime.fromMillisecondsSinceEpoch(map['date']);

  static List<Player> getPlayersFromMap(List value) =>
      value != null ? value.map((map) => Player.fromMap(map)).toList() : [];

  @override
  String toString() {
    return 'Session{name: $name, fieldType: $fieldType, weather: $weather, timeOfDay: $timeOfDay, shoesType: $shoeType, sensorType: $sensorType, players: $players, dateTime: $date}';
  }

  int get numberOfPlayers => players.length;
}
