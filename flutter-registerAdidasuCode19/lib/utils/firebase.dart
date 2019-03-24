import 'package:firebase_database/firebase_database.dart';
import 'package:register_adidas/models/session.dart';

class Database {
  DatabaseReference _db;

  static final Database _singleton = new Database._internal();

  factory Database() {
    return _singleton;
  }

  Database._internal() {
    if (_db == null) {
      _db = FirebaseDatabase.instance.reference();
    }
  }

  Future<bool> checkExist(DatabaseReference ref) async =>
      (await ref.once()).value != null;

  Future<bool> isParticipantInCompetition(
          String competition, String participant) async =>
      await checkExist(
          _db.child("participants").child(competition).child(participant));

  getClassification(String key) =>
      _db.child("classifications").child(key).once();

  getSessions() => _db.child("sessions").orderByKey();

  Future<void> createSession(Session session) => _db
      .child("sessions")
      .child(session.date.millisecondsSinceEpoch.toString())
      .update(session.map());

  removeSession(String key) => _db.child('sessions').child(key).remove();

  updateSession(String key, Session session) =>
      _db.child('sessions').child(key).update(session.map());
}
