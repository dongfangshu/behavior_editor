---@class TaskFightLevel 战斗模板
TaskFightLevel = BaseClass("TaskFightLevel")
function TaskFightLevel:__init( ... )
	-- body

	---@field public levelId number 关卡id
	self.levelId = 1003

	---@class MonsterInfo
	---@field private Id number
	---@field public entityId number
	---@field public position_id string
	---@field public position_name string

	---@field public monsterLevel MonsterInfo[]
	self.monsterLevel ={ 
		{ Id = nil,entityId = 120121,position_id = "",position_name = ""},
		{ Id = nil,entityId = 120121,position_id = "",position_name = ""}
	}
	--
	---@field　private checkRole boolean 获取角色信息1
	self.checkRole1 = true

	---@field　public checkRole boolean 获取角色信息2
	self.checkRole2 = true

	---@field public position string 位置信息
	self.position = "{"pos","10050005"}"
end
function TaskFightLevel:Init( ... )
	-- body
end
function TaskFightLevel:LateInit( ... )
	-- body
end
function TaskFightLevel:Update( ... )
	-- body
end
function TaskFightLevel:Remove( ... )
	-- body
end
function TaskFightLevel:__delete( ... )
	-- body
end
---@function
function TaskFightLevel:GetEntity()
	-- body
end


