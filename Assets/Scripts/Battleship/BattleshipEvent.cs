
using QZVR;

public class BattleshipEvent 
{
   public struct AddBattleship
    {
          public  Battleship Battleship;
        public BattleshipEnum BattleshipEnum;
        public int Count;
    }

    public struct RemoveBattleship
    {
        public Battleship Battleship;
        public BattleshipEnum BattleshipEnum;
        public int Count;
    }

    public struct DispatchBattlenship
    {
        public BattleshipAduentueData data;
        public int Count;
    }
    public struct RemoveDispatchBattlenship
    {
        public BattleshipAduentueData data;
        public int Count;
    }
    public struct ReturnDispatchBattlenship
    {
        public BattleshipAduentueData data;
        public int Count;
    }
}
