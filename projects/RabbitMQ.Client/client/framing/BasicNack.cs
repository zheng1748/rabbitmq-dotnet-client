// This source code is dual-licensed under the Apache License, version
// 2.0, and the Mozilla Public License, version 2.0.
//
// The APL v2.0:
//
//---------------------------------------------------------------------------
//   Copyright (c) 2007-2020 VMware, Inc.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       https://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//---------------------------------------------------------------------------
//
// The MPL v2.0:
//
//---------------------------------------------------------------------------
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
//
//  Copyright (c) 2007-2020 VMware, Inc.  All rights reserved.
//---------------------------------------------------------------------------

namespace RabbitMQ.Client.Framing.Impl
{
    internal sealed class BasicNack : Client.Impl.MethodBase
    {
        public ulong _deliveryTag;
        public bool _multiple;
        public bool _requeue;

        public BasicNack()
        {
        }

        public BasicNack(ulong DeliveryTag, bool Multiple, bool Requeue)
        {
            _deliveryTag = DeliveryTag;
            _multiple = Multiple;
            _requeue = Requeue;
        }

        public override ushort ProtocolClassId => ClassConstants.Basic;
        public override ushort ProtocolMethodId => BasicMethodConstants.Nack;
        public override string ProtocolMethodName => "basic.nack";
        public override bool HasContent => false;

        public override void ReadArgumentsFrom(ref Client.Impl.MethodArgumentReader reader)
        {
            _deliveryTag = reader.ReadLonglong();
            _multiple = reader.ReadBit();
            _requeue = reader.ReadBit();
        }

        public override void WriteArgumentsTo(ref Client.Impl.MethodArgumentWriter writer)
        {
            writer.WriteLonglong(_deliveryTag);
            writer.WriteBit(_multiple);
            writer.WriteBit(_requeue);
            writer.EndBits();
        }

        public override int GetRequiredBufferSize()
        {
            return 8 + 1; // bytes for _deliveryTag, bit fields
        }
    }
}