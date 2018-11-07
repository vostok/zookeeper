using System.Collections;
using System.Collections.Generic;
using System.IO;
using org.apache.jute;
using org.apache.utils;
using org.apache.zookeeper.proto;

namespace org.apache.zookeeper 
{
    /// <summary>
    /// Encodes a composite transaction.  In the wire format, each transaction
    /// consists of a single MultiHeader followed by the appropriate request.
    /// Each of these MultiHeaders has a type which indicates
    /// the type of the following transaction or a negative number if no more transactions
    /// are included.
    /// </summary>
    internal class MultiTransactionRecord : Record, IEnumerable<Op> {
        private readonly List<Op> ops;

        public MultiTransactionRecord() { ops=new List<Op>();}

        public MultiTransactionRecord(IEnumerable<Op> o) {
            ops = new List<Op>(o);
        }

        public IEnumerator<Op> GetEnumerator() {
            return ops.GetEnumerator();
        }

        internal void add(Op op) {
            ops.Add(op);
        }

        public void serialize(OutputArchive archive, string tag) {
            foreach (Op op in ops) {
                MultiHeader h = new MultiHeader(op.get_Type(), false, -1);
                ((Record) h).serialize(archive, tag);
                ZooDefs.OpCode opCode = EnumUtil<ZooDefs.OpCode>.DefinedCast(op.get_Type());
                switch (opCode) {
                    case ZooDefs.OpCode.create:
                        op.toRequestRecord().serialize(archive, tag);
                        break;
                    case ZooDefs.OpCode.delete:
                        op.toRequestRecord().serialize(archive, tag);
                        break;
                    case ZooDefs.OpCode.setData:
                        op.toRequestRecord().serialize(archive, tag);
                        break;
                    case ZooDefs.OpCode.check:
                        op.toRequestRecord().serialize(archive, tag);
                        break;
                    default:
                        throw new IOException("Invalid type of op");
                }
            }
            ((Record) new MultiHeader(-1, true, -1)).serialize(archive, tag);
        }

        public void deserialize(InputArchive archive, string tag) {
            MultiHeader h = new MultiHeader();
            ((Record) h).deserialize(archive, tag);

            while (!h.getDone()) {
                ZooDefs.OpCode opCode = EnumUtil<ZooDefs.OpCode>.DefinedCast(h.get_Type());
                switch (opCode) {
                    case ZooDefs.OpCode.create:
                        CreateRequest cr = new CreateRequest();
                        ((Record) cr).deserialize(archive, tag);
                        add(Op.create(cr.getPath(), cr.getData(), cr.getAcl(), cr.getFlags()));
                        break;
                    case ZooDefs.OpCode.delete:
                        DeleteRequest dr = new DeleteRequest();
                        ((Record) dr).deserialize(archive, tag);
                        add(Op.delete(dr.getPath(), dr.getVersion()));
                        break;
                    case ZooDefs.OpCode.setData:
                        SetDataRequest sdr = new SetDataRequest();
                        ((Record) sdr).deserialize(archive, tag);
                        add(Op.setData(sdr.getPath(), sdr.getData(), sdr.getVersion()));
                        break;
                    case ZooDefs.OpCode.check:
                        CheckVersionRequest cvr = new CheckVersionRequest();
                        ((Record) cvr).deserialize(archive, tag);
                        add(Op.check(cvr.getPath(), cvr.getVersion()));
                        break;
                    default:
                        throw new IOException("Invalid type of op");
                }
                ((Record) h).deserialize(archive, tag);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }

}