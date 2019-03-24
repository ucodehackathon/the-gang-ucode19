import 'package:register_adidas/models/player.dart';
import 'package:register_adidas/models/session.dart';

class Mock {
//   Player(this.height, this.weight, this.age, this.mainFoot, this.shoeSize,
//      this.number);
  static List<Player> get players => [
        Player(175, 70, 23, "left", 42, 35),
        Player(165, 60, 25, "right", 42, 23),
        Player(174, 73, 29, "right", 42, 30)
      ];

//    Session(this.fieldType, this.weather, this.timeOfDay, this.shoeType,
//      this.sensorType, this.players, this.dateTime);
  static List<Session> get sessions => [
        Session('uCode19', fieldTypes[0], weathers[0], timesOfDay[0],
            shoesType[0], "None", 'Description', players, DateTime.now())
      ];

  static List<String> get weathers => ["sunny"];

  static List<String> get fieldTypes => ["grass"];

  static List<String> get timesOfDay => ["morning", "afternoon"];

  static List<String> get shoesType => ["street"];
}
