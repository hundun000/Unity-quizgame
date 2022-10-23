using System;
using System.Collections.Generic;

using hundun.quizlib.exception;
using hundun.quizlib.model.domain.buff;
using hundun.quizlib.prototype.buff;

namespace hundun.quizlib.service
{
	public class BuffService
	{

		private Dictionary<String, BuffPrototype> buffPrototypes = new Dictionary<String, BuffPrototype>();

		public BuffService()
		{


		}

		public void registerBuffPrototype(BuffPrototype prototype)
		{
			buffPrototypes.put(prototype.name, prototype);
		}


		public BuffRuntimeModel generateRunTimeBuff(String buffPrototypeName, int duration)
		{
			if (!buffPrototypes.containsKey(buffPrototypeName))
			{
				throw new NotFoundException("Buff", buffPrototypeName);
			}
			return new BuffRuntimeModel(buffPrototypes.get(buffPrototypeName), duration);
		}

		public ICollection<BuffPrototype> listBuffModels()
		{
			return buffPrototypes.Values;
		}

	}
}






